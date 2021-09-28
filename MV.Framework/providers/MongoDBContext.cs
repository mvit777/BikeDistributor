using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MV.Framework.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MV.Framework.providers
{
    public class MongoDBContext : IMongoDBContext
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }
        public MongoDBContext(MongoSettings configuration)
        {
            try
            {
                _mongoClient = new MongoClient(configuration.Connection);
                _db = _mongoClient.GetDatabase(configuration.DatabaseName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }
    }
}
