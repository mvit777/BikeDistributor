using BikeDistributor.Infrastructure.repositories;
using BikeDistributor.Domain.Entities;
using BikeDistributor.Domain.Models;
using MV.Framework.interfaces;
using MV.Framework.providers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BikeDistributor.Infrastructure.services
{
    [Serializable]
    public class MongoOrderService : IMongoService
    {
        public MongoDBContext Context { get => _context; set => _context = value; }

        private MongoDBContext _context;

        private OrderRepository _orderRepo;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public MongoOrderService(MongoDBContext context)
        {
            _context = context;
            _orderRepo = new OrderRepository(_context);
        }

        [JsonConstructor]
        public MongoOrderService(string context, string serviceName)
        {
            File.WriteAllText(@"c:\temp\context_as_string", context);
        }

        public async Task<MongoEntityOrder> AddOrderAsync(Order order)
        {
            var meo = new MongoEntityOrder(order);
            //meb.TotalPrice = bike.Price;
            await _orderRepo.Create(meo);
            
            return await _orderRepo.Get(order.OrderId.ToString(), true);
        }

        public async Task<List<MongoEntityOrder>> Get()
        {
            return (List<MongoEntityOrder>)await _orderRepo.Get();
        }
        public async Task<MongoEntityOrder> Get(string id)
        {
            return await _orderRepo.Get(id, true);
        }
        public MongoEntityOrder Update(MongoEntityOrder obj)
        {
            _orderRepo.Update(obj);

            return obj;
        }
        public void Delete(string id)
        {
            _orderRepo.Delete(id, true);
        }
    }
}


