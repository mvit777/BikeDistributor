using BikeDistributor.Domain;
using BikeDistributor.Infrastructure.interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeDistributor.Domain.Models;
using BikeDistributor.Domain.Entities;
using BikeDistributor.Infrastructure.factories;

namespace BikeDistributor.Infrastructure.core
{
    public class JsonUtils
    {
        public static List<BikeOption> GetListFromJArrayOption(string json)
        {
            List<BikeOption> options = new List<BikeOption>();
            List<JToken> jtokens = JArray.Parse(json).ToList();
            foreach (JToken token in jtokens)
            {
                options.Add(JsonConvert.DeserializeObject<BikeOption>(token.ToString()));
            }
            return options;
        }
        public static List<Discount> GetListFromJArrayDiscounts(string json)
        {
            List<Discount> discounts = new List<Discount>();
            List<JToken> jtokens = JArray.Parse(json).ToList();
            foreach (JToken token in jtokens)
            {
                discounts.Add(JsonConvert.DeserializeObject<Discount>(token.ToString()));
            }
            return discounts;
        }
        public static List<Order> GetListFromJArrayOrders(string json)
        {
            List<Order> orders = new List<Order>();
            List<JToken> jtokens = JArray.Parse(json).ToList();
            foreach (JToken token in jtokens)
            {
                orders.Add(JsonConvert.DeserializeObject<Order>(token.ToString()));
            }
            return orders;
        }

        public static List<IBike> GetListFromJArrayIBike(string json)
        {
            List<IBike> bikes = new List<IBike>();
            List<JToken> jtokens = JArray.Parse(json).ToList();
            foreach (JToken token in jtokens)
            {
                if ((bool)token["isStandard"] == true)
                {
                    var Bike = (Bike)JsonConvert.DeserializeObject<Bike>(token.ToString());
                    bikes.Add(Bike);
                }
                else
                {
                    var Bike = (BikeVariant)JsonConvert.DeserializeObject<BikeVariant>(token.ToString());
                    bikes.Add(Bike);
                }

            }
            return bikes;
        }

        public static IBike DeserializeIBikeModel(string json)
        {
            JObject o = JObject.Parse(json);
            IBike b = null;
            if (o.Value<bool>("isStandard") == true)
            {
                b = JsonConvert.DeserializeObject<Bike>(o.ToString());
            }
            else
            {
                b = JsonConvert.DeserializeObject<BikeVariant>(o.ToString());
            }
            return b;
        }
        public static List<MongoEntityBikeOption> DeserializeMongoEntityBikeOptionList(string json)
        {
            List<MongoEntityBikeOption> mebs = new List<MongoEntityBikeOption>();
            List<JToken> jtokens = JArray.Parse(json).ToList();
            foreach (JToken token in jtokens)
            {
                MongoEntityBikeOption mebo = DeserializeIBikeOptionEntity(token);
                mebs.Add(mebo);

            }
            return mebs;
        }

        private static MongoEntityBikeOption DeserializeIBikeOptionEntity(JToken token)
        {
            var BikeOption = JsonConvert.DeserializeObject<BikeOption>(token["bikeOption"].ToString());
            return new MongoEntityBikeOption(BikeOption);
        }

        public static List<MongoEntityBike> DeserializeMongoEntityBikeList(string json)
        {
            List<MongoEntityBike> mebs = new List<MongoEntityBike>();
            List<JToken> jtokens = JArray.Parse(json).ToList();
            foreach (JToken token in jtokens)
            {
                MongoEntityBike meb = DeserializeIBikeEntity(token);
                mebs.Add(meb);

            }
            return mebs;
        }

        public static MongoEntityBike DeserializeIBikeEntity(JToken token)
        {
            JObject jo = JObject.Parse(token.ToString());
            bool isStandard = jo["isStandard"] == null ? (bool)jo["IsStandard"] : (bool)jo["isStandard"];
            if (isStandard == true)
            {
                return DeserializeBikeEntity(token);
            }
            return DeserializeBikeVariantEntity(token);
        }

        public static MongoEntityBike DeserializeBikeEntity(JToken token)
        {
            var obj = JObject.Parse(token.ToString());
            string objBike = obj["bike"] == null ? obj["Bike"].ToString() : obj["bike"].ToString();
            var Bike = JsonConvert.DeserializeObject<Bike>(objBike);
            Bike.RecalculatePrice();
            return new MongoEntityBike(Bike);
        }

        public static MongoEntityBike DeserializeBikeVariantEntity(JToken token)
        {
            var obj = JObject.Parse(token.ToString());
            //TODO: DESERIALZIE BikeOptions and re-add to bike
            string bikeKey = obj["bike"] == null ? "Bike" : "bike";
            string optionsLabel = obj["selectedOptions"] == null ? "SelectedOptions" : "selectedOptions";
            var Bike = JsonConvert.DeserializeObject<BikeVariant>(obj[bikeKey].ToString());
            List<JToken> joptions = JArray.Parse(obj[bikeKey][optionsLabel].ToString()).ToList();
            var options = new List<BikeOption>();
            foreach (JToken o in joptions)
            {
                var option = JsonConvert.DeserializeObject<BikeOption>(o.ToString());
                options.Add(option);
            }
            Bike.SelectedOptions = options;
            Bike.RecalculatePrice();
            var meb = new MongoEntityBike(Bike);
            //meb.IsStandard = false;
            //meb.TotalPrice = (int)token["totalPrice"];
            return meb;
        }
        /// <summary>
        /// https://stackoverflow.com/questions/3445784/copy-the-property-values-to-another-object-with-c-sharp
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="sourceItem"></param>
        /// <returns></returns>
        public static TTarget Convert<TSource, TTarget>(TSource sourceItem, IEnumerable<string> propNamesToIgnore = null)
        {
            if (null == sourceItem)
            {
                return default(TTarget);
            }

            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace, };

            if(propNamesToIgnore == null)
            {
                var serializedObject = JsonConvert.SerializeObject(sourceItem, deserializeSettings);

                return JsonConvert.DeserializeObject<TTarget>(serializedObject);
            }
            else
            {
                var serializedObject = JsonConvert.SerializeObject(sourceItem, new JsonSerializerSettings()
                { ContractResolver = new IgnorePropertiesResolver(propNamesToIgnore) });

                return JsonConvert.DeserializeObject<TTarget>(serializedObject);
            }
            
            
        }

        //public string GenerateBsonId(string id)
        //{
        //    BsonSerializer.RegisterIdGenerator(typeof(string), new StringObjectIdGenerator());
        //    //BsonSerializer.LookupIdGenerator(bike.Model.GetType());
        //}
    }

    //https://stackoverflow.com/questions/67044973/using-newtonsoft-json-custom-converters-to-read-json-with-different-input
    #region "dead code"

    //class BikeVariantConverter : JsonConverter
    //{
    //    public override bool CanConvert(Type objectType)
    //    {
    //        return typeof(BikeVariant).IsAssignableFrom(objectType);
    //    }

    //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //    {
    //        JObject obj = JObject.Load(reader);


    //        //string type = (string)obj["$type"];//fix it: we have no $type and _t disappears
    //        string type = "BikeVariant";
    //        BikeVariant bv;
    //        if (type.Contains(nameof(BikeVariant)))
    //        {
    //            var model = (string)obj["model"];
    //            var brand = (string)obj["brand"];
    //            var price = (int)obj["price"];
    //            List<BikeOption> options = new List<BikeOption>();
    //            //List<JToken> joptions = JArray.Parse(obj["selectedOptions"].ToString()).ToList();
    //            //foreach (JToken jo in joptions)
    //            //{
    //            //    var option = JsonConvert.DeserializeObject<BikeOption>(jo.ToString());
    //            //    options.Add(option);
    //            //}
    //            bv = new BikeVariant(brand, model, price, options);
    //        }
    //        else
    //        {
    //            return null;
    //        }

    //        serializer.Populate(obj.CreateReader(), bv);


    //        return bv;
    //    }

    //    public override bool CanWrite => false;

    //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //============MEB CONVERTER=======================
    //class MongoEntityBikeConverter : JsonConverter
    //{
    //    public override bool CanConvert(Type objectType)
    //    {
    //        return typeof(Bike).IsAssignableFrom(objectType);
    //    }

    //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //    {
    //        JObject obj = JObject.Load(reader);

    //        //string type = (string)obj["$type"];//fix it: we have no $type and _t disappears
    //        string type = "BikeVariant";
    //        BikeVariant bv;
    //        if (type.Contains(nameof(BikeVariant)))
    //        {
    //            var model = (string)obj["model"];
    //            var brand = (string)obj["brand"];
    //            var price = (int)obj["price"];
    //            List<BikeOption> options = new List<BikeOption>();
    //            List<JToken> joptions = JArray.Parse(obj["selectedOptions"].ToString()).ToList();
    //            foreach (JToken jo in joptions)
    //            {
    //                var option = JsonConvert.DeserializeObject<BikeOption>(jo.ToString());
    //                options.Add(option);
    //            }
    //            bv = new BikeVariant(brand, model, price, options);
    //        }
    //        else
    //        {
    //            return null;
    //        }

    //        serializer.Populate(obj.CreateReader(), bv);


    //        return bv;
    //    }

    //    public override bool CanWrite => false;

    //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
    #endregion
}
