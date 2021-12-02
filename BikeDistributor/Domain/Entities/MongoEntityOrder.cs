using BikeDistributor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeDistributor.Domain.Entities
{
    public class MongoEntityOrder
    {
        public MongoEntityOrder(Order order)
        {
            this.order = order;
        }

        public string Id { get; set; }
        public Order order { get; set; }
    }
}
