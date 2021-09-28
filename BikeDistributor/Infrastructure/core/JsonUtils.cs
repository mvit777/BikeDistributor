using BikeDistributor.Domain;
using BikeDistributor.Infrastructure.interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        //public string GenerateBsonId(string id)
        //{
        //    BsonSerializer.RegisterIdGenerator(typeof(string), new StringObjectIdGenerator());
        //    //BsonSerializer.LookupIdGenerator(bike.Model.GetType());
        //}
    }
}
