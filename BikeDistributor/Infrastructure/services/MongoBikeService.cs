using BikeDistributor.Domain;
using BikeDistributor.Domain.Entities;
using BikeDistributor.Infrastructure.interfaces;
using BikeDistributor.Infrastructure.repositories;
using MV.Framework.interfaces;
using MV.Framework.providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor.Infrastructure.services
{
    public class MongoBikeService : IMongoService
    {
        private MongoDBContext _context;
        private BikeRepository _bikeRepo;
        
        /// <summary>
        /// TODO: do this service work only in terms of bikes and not meb. See addAsync for better explanation
        /// </summary>
        /// <param name="context"></param>
        public MongoBikeService(MongoDBContext context)
        {
            _context = context;
            _bikeRepo = new BikeRepository(_context);
        }

        public async Task<MongoEntityBike> AddBikeAsync(IBike bike)
        {
            var meb = new MongoEntityBike(bike);
            //meb.TotalPrice = bike.Price;
            await _bikeRepo.Create(meb);

            return await _bikeRepo.Get(bike.Model, true);
        }

        public async Task<List<MongoEntityBike>> Get()
        {
           return (List<MongoEntityBike>)await _bikeRepo.Get();
        }
        public async Task<MongoEntityBike> Get(string id)
        {
            return await _bikeRepo.Get(id, true);
        }
        public MongoEntityBike Update(MongoEntityBike obj)
        {

            obj.TotalPrice = obj.Bike.Price;//ok
            _bikeRepo.Update(obj);

            return obj;
        }
        public void Delete(string id)
        {
            _bikeRepo.Delete(id, true);
        }
    }
}
