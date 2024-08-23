﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VillaTour.Domain.Entities;

namespace VillaTour.Application.Common.Interfaces
{
    public interface IAmenityRepository : IRepository<Amenity>
    {
      
        void Update(Amenity entity);
       
    }
}