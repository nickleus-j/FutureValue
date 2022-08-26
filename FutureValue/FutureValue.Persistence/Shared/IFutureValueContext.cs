using FutureValue.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutureValue.Persistence.Shared
{
    public  interface IFutureValueContext 
    {
        public DbSet<AspUser> AspUser { get; set; }
        public DbSet<ProjectionForm> ProjectionForm { get; set; }
        void Save();
        void SeedData();
    }
}
