using MongoDB.Driver;
using System;
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
    }
}
