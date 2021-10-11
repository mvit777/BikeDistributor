using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MV.Framework.providers
{
    public class MongoSettings
    {
        public string Connection { get; set; }
        public string DatabaseName { get; set; }
        public string servicesNameSpace { get; set; }
        public string servicesDll { get; set; }
        public ArrayList Services { get; set; }
        public ArrayList BsonTypes { get; set; }
    }
}
