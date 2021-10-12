using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MV.Framework.interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
        
        public MongoSettings MongoSettings { get; set; }

        [JsonConstructor]
        public MongoDBContext(MongoSettings configuration)
        {
            try
            {
                File.WriteAllText(@"c:\temp\configuration.txt",JsonConvert.SerializeObject(configuration));
                MongoSettings = configuration;
                _mongoClient = new MongoClient(MongoSettings.Connection);
                _db = _mongoClient.GetDatabase(MongoSettings.DatabaseName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.Source);
            }


        }

        /// <summary>
        /// todo remove as not needed
        /// </summary>
        /// <param name="connection_string"></param>
        public MongoDBContext(string connectionString, string dbName)
        {
            try
            {
                var clientSettings = MongoClientSettings.FromConnectionString(connectionString);
                _mongoClient = new MongoClient(clientSettings);
                _db = _mongoClient.GetDatabase(dbName);
            }
            catch(Exception ex)
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
