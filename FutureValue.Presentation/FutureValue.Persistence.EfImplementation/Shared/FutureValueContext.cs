using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using FutureValue.Persistence.Shared;
using FutureValue.Domain;

namespace FutureValue.Persistence.EfImplementation.Shared
{
    public class FutureValueContext : DbContext, IFutureValueContext
    {
        public DbSet<AspUser> AspUser { get; set; }
        public DbSet<ProjectionForm> ProjectionForm { get; set; }
        public FutureValueContext(DbContextOptions<FutureValueContext> options) : base(options)
        {
        }
        public FutureValueContext()
        {
        }
        public void Save()
        {
            this.SaveChanges();
        }
    }
}
