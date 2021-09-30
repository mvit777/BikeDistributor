using BikeDistributor.Infrastructure.interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor.Domain.Entities
{
    //[BsonDiscriminator(Required = true)]
    //[BsonKnownTypes(typeof(Bike), typeof(BikeVariant))]
    public class MongoEntityBike
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }
        public int TotalPrice { get; set; }
        public IBike Bike { get; set; }
        public MongoEntityBike(IBike bike)
        {
            Bike = bike;
            Id = Bike.Model;
             TotalPrice = Bike.Price;          
        }

    }
}
