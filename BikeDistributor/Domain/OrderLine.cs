using BikeDistributor.Infrastructure.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor.Domain
{
    public class OrderLine
    {
        public OrderLine(IBike bike, int quantity)
        {
            Bike = bike;
            Quantity = quantity;
        }

        public IBike Bike { get; }
        public int Quantity { get; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
