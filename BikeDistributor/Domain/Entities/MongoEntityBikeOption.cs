using BikeDistributor.Infrastructure.interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeDistributor.Domain.Entities
{
    public class MongoEntityBikeOption
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }
        public IBikeOption BikeOption { get; set; }

        public MongoEntityBikeOption(IBikeOption bikeOption)
        {
            BikeOption = bikeOption;
            Id = BikeOption.Name;
        }
    }
}
