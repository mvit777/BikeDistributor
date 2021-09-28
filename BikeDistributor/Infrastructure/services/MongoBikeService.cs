using BikeDistributor.Domain;
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
        

        public MongoBikeService(MongoDBContext context)
        {
            _context = context;
            _bikeRepo = new BikeRepository(_context);
        }

        public async Task AddBikeAsync(IBike bike)
        {
            var meb = new MongoEntityBike(bike);
          
            await _bikeRepo.Create(meb);
        }

        public async Task<List<MongoEntityBike>> Get()
        {
           return (List<MongoEntityBike>)await _bikeRepo.Get();
        }
    }
}
