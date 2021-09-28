using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor.Domain
{
    public class Discount
    {
        public int QuantityThreeShold { get; set; }
        public int UnitPriceThreeShold { get; set; }
        public double Percentage { get; set; }
    }
}
