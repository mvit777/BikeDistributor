﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor.Infrastructure.interfaces
{
    public interface IBikeOption
    {
        string Name { get; set; }
        string Description { get; set; }
        int Price { get; set; }
    }
}
