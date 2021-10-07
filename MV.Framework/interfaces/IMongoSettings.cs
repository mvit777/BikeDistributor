using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MV.Framework.interfaces
{
    public interface IMongoSettings
    {
        public string Connection { get; set; }
        public string DatabaseName { get; set; }
        public string servicesNameSpace { get; set; }
        public ArrayList Services { get; set; }
    }
}
