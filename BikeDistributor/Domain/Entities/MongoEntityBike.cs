using BikeDistributor.Domain.Models;
using BikeDistributor.Infrastructure.interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MV.Framework.providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
        public bool IsStandard { get; set; }

        //[BsonSerializer(typeof(CustomBsonSerializer))]
        //[BsonElement("Bike")]
        public IBike Bike { get; set; }
        public MongoEntityBike(IBike bike)
        {
            Bike = bike;
            Id = Bike.Model;
            TotalPrice = Bike.Price;
            IsStandard = Bike.isStandard;
        }

        public MongoEntityBike()
        {

        }

    }
}
