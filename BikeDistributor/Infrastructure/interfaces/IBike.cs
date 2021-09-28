using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor.Infrastructure.interfaces
{
    public interface IBike
    {
        string Brand { get; }
        string Model { get; }
        int Price { get; }
        bool isStandard { get; }
    }
}
