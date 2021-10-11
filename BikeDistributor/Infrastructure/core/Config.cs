using MV.Framework.providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor.Infrastructure.core
{
    public class Config : MV.Framework.BaseConfig
    {
        public MongoSettings DefaultMongoSettings { get; set; }

        protected new string _rootElement = "Settings";

        public Config(string jsonFilePath) : base(jsonFilePath)
        {

        }

        public MongoSettings LoadMongoSettings(int index)
        {
            return this.GetClassObject<MongoSettings>("Mongo", index);
        }

        public string GetMongoServiceIdentity(string serviceName)
        {
            return DefaultMongoSettings.servicesNameSpace + "." + serviceName + ", " + DefaultMongoSettings.servicesDll;
        }
    }
}
