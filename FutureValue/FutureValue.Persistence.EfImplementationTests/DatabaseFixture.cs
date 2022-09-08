using FutureValue.Domain.Entities;
using FutureValue.Persistence.EfImplementation.AspUsers;
using FutureValue.Persistence.EfImplementation.Shared;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutureValue.Persistence.EfImplementationTests
{
    public class DatabaseFixture : IDisposable
    {
        public FutureValueContext webContext { get; set; }
        public string defaultPassword => "default 123";
        public void seed()
        {
            
            UserAuthenticator authenticator = new UserAuthenticator();
            DbContextOptions<FutureValueContext> dbContextOptions = new DbContextOptionsBuilder<FutureValueContext>()
                .UseInMemoryDatabase(databaseName: "FutureValue").Options;
            webContext = new FutureValueContext(dbContextOptions);

            List<ProjectionForm> projectionForms = new List<ProjectionForm>();
            projectionForms.Add(new ProjectionForm
            {
                IncrementalRate = 10,
                LowerBoundInterest = 0,
                UpperBoundInterest = 50
                ,
                IsActive = true,
                DateCreated = DateTime.Now,
                MaturityYears = 5
                ,
                Name = "start at the bottom but end high",
                PresetValue = 25000
            });
            projectionForms.Add(new ProjectionForm
            {
                IncrementalRate = 5,
                LowerBoundInterest = 5,
                UpperBoundInterest = 15,
                IsActive = true,
                MaturityYears = 3,
                Name = "Watch out for variable Interest rates",
                DateCreated = DateTime.Now,
                AspUserId=1,
                PresetValue = 5000
            });
            projectionForms.Add(new ProjectionForm
            {
                IncrementalRate = 5,
                LowerBoundInterest = 5,
                UpperBoundInterest = 15,
                IsActive = false,
                MaturityYears = 3,
                Name = "Watch out for variable Interest rates",
                DateCreated = DateTime.Now,
                PresetValue = 5000
            });
            
            if (webContext.ProjectionForm.Count() == 0)
            {
                webContext.ProjectionForm.AddRange(projectionForms);
                webContext.SaveChanges();
            }
            if(webContext.AspUser.Count() == 0)
            {
                webContext.AspUser.Add(new AspUser
                {
                    ID = 1,
                    IsActive = true,
                    UserName = "Sample",
                    UserPassword = authenticator.HashPassword(defaultPassword)
                });
                webContext.SaveChanges();
            }
        }
        public DatabaseFixture()
        {
            seed();
        }

        public void Dispose()
        {
            webContext?.Dispose();
        }
    }
}
