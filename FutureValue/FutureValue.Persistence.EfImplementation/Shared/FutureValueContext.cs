using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using FutureValue.Persistence.Shared;
using FutureValue.Domain.Entities;
using FutureValue.Persistence.EfImplementation.AspUsers;

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
        public void SeedData()
        {
            using (this)
            {
                if (!this.ProjectionForm.Any())
                {
                    ProjectionForm.Add(new ProjectionForm
                    {
                        DateCreated = DateTimeOffset.Now,
                        IncrementalRate = 10,
                        IsActive = true,
                        LowerBoundInterest = 10,
                        UpperBoundInterest = 50,
                        MaturityYears = 5,
                        PresetValue = 1000,
                        Name = "Sample"
                    });
                }
                if (!this.AspUser.Any())
                {
                    UserAuthenticator userAuthenticator = new UserAuthenticator();
                    AspUser.Add(new AspUser
                    {
                        IsActive = true,
                        UserName="sample user",
                        UserPassword= userAuthenticator.HashPassword("default 123")
                    });
                }
                this.SaveChanges();
            }
        }
    }
}
