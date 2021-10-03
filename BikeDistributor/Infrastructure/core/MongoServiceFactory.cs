using MV.Framework.interfaces;
using MV.Framework.providers;
using System;
using System.Linq;

namespace BikeDistributor.Infrastructure.core
{
    public class MongoServiceFactory
    {
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="mongoUrl"></param>
        /// <param name="databaseName"></param>
        /// <param name="ns">services namespace</param>
        /// <param name="className"></param>
        /// <returns>Selected MongoService with appropriate MongoContext</returns>
        public static IMongoService GetMongoService(string mongoUrl, string databaseName, string ns, string className)
        {
            var context = GetMongoDBContext(mongoUrl, databaseName);
            //string ns = "BikeDistributor.Infrastructure.services";
            var myClassType = Type.GetType(String.Format("{0}.{1}", new object[] { ns, className }));
            var parametrizedCtor = myClassType
            .GetConstructors()
            .FirstOrDefault(c => c.GetParameters().Length > 0);
            try
            {
                var instance = parametrizedCtor.Invoke(new object[] { context });
                return (IMongoService)instance;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " NOT FOUND CLASS IS OF TYPE " + myClassType + " namespace is " + ns);
            }
        }

        public static IMongoService GetMongoService(MongoDBContext context, string fullyQualifiedServiceClassName)
        {
            var parametrizedCtor = fullyQualifiedServiceClassName.GetType()
            .GetConstructors()
            .FirstOrDefault(c => c.GetParameters().Length > 0);
            try
            {
                var instance = parametrizedCtor.Invoke(new object[] { context });
                return (IMongoService)instance;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " NOT FOUND CLASS IS OF TYPE " + fullyQualifiedServiceClassName);
            }
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
