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
            if ((bool)token["isStandard"] == true)
            {
                return DeserializeBikeEntity(token);
            }
            return DeserializeBikeVariantEntity(token);
        }

        public static MongoEntityBike DeserializeBikeEntity(JToken token)
        {
            var Bike = JsonConvert.DeserializeObject<Bike>(token["bike"].ToString());
            Bike.RecalculatePrice();
            return new MongoEntityBike(Bike);
        }

        public static MongoEntityBike DeserializeBikeVariantEntity(JToken token)
        {
            //TODO: DESERIALZIE BikeOptions and re-add to bike
            var Bike = JsonConvert.DeserializeObject<BikeVariant>(token["bike"].ToString()/*, new BikeVariantConverter()*/);
            List<JToken> joptions = JArray.Parse(token["selectedOptions"].ToString()).ToList();
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
