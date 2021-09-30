using BikeDistributor.Domain;
using MV.Framework.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeDistributor.Domain.Entities;

namespace BikeDistributor.Infrastructure.interfaces
{
    public interface IBikeRepository : IBaseRepository<MongoEntityBike>
    {

    }
}
