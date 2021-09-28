using BikeDistributor.Domain;
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
    public class BikeRepository : BaseRepository<MongoEntityBike>, IBikeRepository
    {
        public BikeRepository(IMongoDBContext context) : base(context)
        {
        }
    }
}
