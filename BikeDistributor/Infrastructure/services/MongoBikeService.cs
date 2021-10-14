using BikeDistributor.Domain;
using BikeDistributor.Domain.Entities;
using BikeDistributor.Domain.Models;
using BikeDistributor.Infrastructure.interfaces;
using BikeDistributor.Infrastructure.repositories;
using MV.Framework.interfaces;
using MV.Framework.providers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor.Infrastructure.services
{
    [Serializable]
    public class MongoBikeService : IMongoService
    {
        public MongoDBContext Context { get => _context; set => _context=value; }
        
        private MongoDBContext _context;
        
        private BikeRepository _bikeRepo;

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public MongoBikeService(MongoDBContext context)
        {
            _context = context;
            _bikeRepo = new BikeRepository(_context);
        }

        [JsonConstructor]
        public MongoBikeService(string context, string serviceName)
        {
            File.WriteAllText(@"c:\temp\context_as_string", context);
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
            if (obj.Bike.isStandard == false)
            {
                var bv = (BikeVariant)obj.Bike;
                obj.SelectedOptions = bv.SelectedOptions;
                bv.RecalculatePrice();
                obj.TotalPrice = bv.Price;//ok
            }
            
            _bikeRepo.Update(obj);

            return obj;
        }
        public void Delete(string id)
        {
            _bikeRepo.Delete(id, true);
        }
    }
}
