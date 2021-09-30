using BikeDistributor.Domain;
using BikeDistributor.Domain.Entities;
using BikeDistributor.Infrastructure.interfaces;
using MV.Framework.interfaces;
using MV.Framework.providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor.Infrastructure.repositories
{
    /// <summary>
    /// Never uses this class directly. I'm not (yet) making this class internal 
    /// for the sole purpose of testing.
    /// Use (Mongo)BikeService instead.
    /// </summary>
    public class BikeRepository : BaseRepository<MongoEntityBike>, IBikeRepository
    {
        internal BikeRepository(IMongoDBContext context) : base(context)
        {
        }
    }
}
