using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor.Infrastructure.core
{
    public class Config : MV.Framework.TestConfig
    {
        public Config(string jsonFilePath) : base(jsonFilePath)
        {

        }
    }
}
