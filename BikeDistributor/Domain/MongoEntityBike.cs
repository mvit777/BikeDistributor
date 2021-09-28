using BikeDistributor.Infrastructure.interfaces;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor.Domain
{
    //[BsonDiscriminator(Required = true)]
    //[BsonKnownTypes(typeof(Bike), typeof(BikeVariant))]
    public class MongoEntityBike
    {
        [BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public IBike Bike { get; set; }
        public MongoEntityBike(IBike bike)
        {
            Bike = bike;
            Id = Bike.Model;
        }

    }
}
