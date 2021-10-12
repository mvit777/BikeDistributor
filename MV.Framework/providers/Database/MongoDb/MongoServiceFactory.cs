using MV.Framework.interfaces;
using MV.Framework.providers;
using System;
using System.Linq;

namespace MV.Framework.providers
{
    public class MongoServiceFactory
    {
        /// <summary>
        /// self contained but verbose way to instantiate a new MongoService
        /// </summary>
        /// <param name="mongoUrl"></param>
        /// <param name="databaseName"></param>
        /// <param name="ns">services namespace</param>
        /// <param name="className"></param>
        /// <returns>Selected MongoService with appropriate MongoContext</returns>
        public static IMongoService GetMongoService(string mongoUrl, string databaseName, string ns, string className, string assemblyName)
        {
            var context = GetMongoDBContext(mongoUrl, databaseName);
            //string ns = "BikeDistributor.Infrastructure.services";
            var myClassType = Type.GetType(String.Format("{0}.{1}, {2}", new object[] { ns, className, assemblyName }));
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
                throw new Exception(ex.Message + " NOT FOUND CLASS IS OF TYPE " + myClassType + " namespace is " + ns + " assembly is " + assemblyName);
            }
        }
        /// <summary>
        /// Recomended way to instantiate a Mongo service
        /// </summary>
        /// <param name="context"></param>
        /// <param name="className">just the classname without fullyqualified path</param>
        /// <returns></returns>
        public static IMongoService GetMongoService(MongoDBContext context, string className)
        { 
            var parametrizedCtor = Type.GetType(String.Format("{0}.{1}, {2}", new object[] { context.MongoSettings.servicesNameSpace, className, context.MongoSettings.servicesDll }))
            .GetConstructors()
            .FirstOrDefault(c => c.GetParameters().Length > 0);
            try
            {
                var instance = parametrizedCtor.Invoke(new object[] { context });
                return (IMongoService)instance;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " NOT FOUND CLASS IS OF TYPE " + className + " namespace is " + context.MongoSettings.servicesNameSpace + " assembly is " + context.MongoSettings.servicesDll);
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
