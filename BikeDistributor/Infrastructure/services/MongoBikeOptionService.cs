﻿using System.Collections.Generic;
using MV.Framework.interfaces;
using MV.Framework.providers;
using System.Threading.Tasks;
using BikeDistributor.Infrastructure.repositories;
using BikeDistributor.Infrastructure.interfaces;
using BikeDistributor.Domain.Entities;

namespace BikeDistributor.Infrastructure.services
    {
        public class MongoBikeOptionService : IMongoService
        {
            private MongoDBContext _context;
            private BikeOptionRepository _bikeOptionRepo;

            /// <summary>
            /// TODO: do this service work only in terms of bikes and not meb. See addAsync for better explanation
            /// </summary>
            /// <param name="context"></param>
            public MongoBikeOptionService(MongoDBContext context)
            {
                _context = context;
                _bikeOptionRepo = new BikeOptionRepository(_context);
            }

            public async Task AddBikeAsync(IBikeOption option)
            {
                var meb = new MongoEntityBikeOption(option);
                
                await _bikeOptionRepo.Create(meb);
            }

            public async Task<List<MongoEntityBike>> Get()
            {
                return (List<MongoEntityBike>)await _bikeOptionRepo.Get();
            }
            public async Task<MongoEntityBikeOption> Get(string id)
            {
                return await _bikeOptionRepo.Get(id, true);
            }
            public void Update(MongoEntityBikeOption obj)
            {
                _bikeOptionRepo.Update(obj);
            }
            public void Delete(string id)
            {
                _bikeOptionRepo.Delete(id, true);
            }
        }
    }

