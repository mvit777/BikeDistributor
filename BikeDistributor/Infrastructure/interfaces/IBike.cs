using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor.Infrastructure.interfaces
{
    public interface IBike
    {
        string Brand { get; set; }
        string Model { get; set; }
        int Price { get; }
        int BasePrice { get; set; }
        bool isStandard { get;  }
    }
}
