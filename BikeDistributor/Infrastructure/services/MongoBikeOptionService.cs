using System.Collections.Generic;
using MV.Framework.interfaces;
using MV.Framework.providers;
using System.Threading.Tasks;
using BikeDistributor.Infrastructure.repositories;
using BikeDistributor.Infrastructure.interfaces;
using BikeDistributor.Domain.Entities;
using System;

namespace BikeDistributor.Infrastructure.services
{
    [Serializable]
    public class MongoBikeOptionService : IMongoService
    {
        public MongoDBContext Context { get => _context; set => _context=value; }
        private MongoDBContext _context;
        private BikeOptionRepository _bikeOptionRepo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public MongoBikeOptionService(MongoDBContext context)
        {
            _context = context;
            _bikeOptionRepo = new BikeOptionRepository(_context);
        }

        public async Task<MongoEntityBikeOption> AddBikeOptionAsync(IBikeOption option)
        {
            var meb = new MongoEntityBikeOption(option);

            await _bikeOptionRepo.Create(meb);

            return await _bikeOptionRepo.Get(option.Name, true);
        }

        public async Task<List<MongoEntityBikeOption>> Get()
        {
            return (List<MongoEntityBikeOption>)await _bikeOptionRepo.Get();
        }
        public async Task<MongoEntityBikeOption> Get(string id)
        {
            return await _bikeOptionRepo.Get(id, true);
        }
        public MongoEntityBikeOption Update(MongoEntityBikeOption obj)
        {
            _bikeOptionRepo.Update(obj);

            return obj;
        }
        public void Delete(string id)
        {
            _bikeOptionRepo.Delete(id, true);
        }
    }
}


