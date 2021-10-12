using MV.Framework.interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using MV.Framework.providers;
using Newtonsoft.Json.Linq;

namespace MV.Framework.providers
{
    public class MongoServiceInstanceRegister : IServiceRegister
    {
        private Dictionary<string, string> Register;

        public void SetServiceInstance(IMongoService instance, string key)//cannot serialize an interface
        {
            if(Register == null)
            {
                Register = new Dictionary<string, string>();
            }
            //var js = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented };
            string obj = JsonConvert.SerializeObject(instance);
            if (Register.ContainsKey(key)==false)
            {
                Register.Add(key, obj);
            } 
        }
        /// <summary>
        /// Type _type = Type.GetType("Namespace.MyClass, MyAssembly");
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type">a string in the form "Namespace.MyClass, MyAssembly"</param>
        /// <returns></returns>
        public IMongoService GetServiceInstance(string key, string type)
        {
            if (Register == null)
            {
                Register = new Dictionary<string, string>();
                return null;
            }
            try
            {
                string obj = Register[key];
                Type _type = Type.GetType(type);
                //var service = JsonConvert.DeserializeObject(obj, _type);//probably throws exception
                JObject jo = JObject.Parse(obj);
                var mongoSettings = JsonConvert.DeserializeObject<MongoSettings>(jo["Context"]["MongoSettings"].ToString());
                var mongoContext = new MongoDBContext(mongoSettings);
                IMongoService service = MongoServiceFactory.GetMongoService(mongoContext, _type.Name);

                return service;
            }
            catch(Exception ex)
            {
                throw new Exception("service " + key + " not found " + ex.Message);
            }
            
        }


    }
}
