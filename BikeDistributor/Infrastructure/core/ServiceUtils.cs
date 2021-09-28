using BikeDistributor.Infrastructure.services;
using MV.Framework.interfaces;
using MV.Framework.providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor.Infrastructure.core
{
    public static class ServiceUtils
    {
        public static IMongoService GetBikeMongoService(string mongoUrl, string databaseName)
        {
            var context = GetMongoDBContext(mongoUrl, databaseName);

            return new MongoBikeService(context);
        }

        public static MongoDBContext GetMongoDBContext(string mongoUrl, string databaseName)
        {
            var mg = new MongoSettings();
            mg.Connection = mongoUrl;
            mg.DatabaseName = databaseName;

            return new MongoDBContext(mg);
        }
    }
}
