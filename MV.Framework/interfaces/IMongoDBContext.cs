using MongoDB.Driver;
using MV.Framework.providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MV.Framework.interfaces
{
    public interface IMongoDBContext
    {
        public MongoSettings MongoSettings { get; set; }
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
