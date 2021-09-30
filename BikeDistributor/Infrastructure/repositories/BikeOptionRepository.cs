using BikeDistributor.Domain.Entities;
using BikeDistributor.Infrastructure.interfaces;
using MV.Framework.interfaces;
using MV.Framework.providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeDistributor.Infrastructure.repositories
{
    internal class BikeOptionRepository : BaseRepository<MongoEntityBikeOption>, IBikeOptionRepository
    {
        internal BikeOptionRepository(IMongoDBContext context) : base(context)
        {

        }
    }
}
