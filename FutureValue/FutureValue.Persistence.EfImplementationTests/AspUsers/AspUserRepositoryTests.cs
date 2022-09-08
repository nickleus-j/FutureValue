using Xunit;
using FutureValue.Persistence.EfImplementation.AspUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FutureValue.Persistence.EfImplementation.Shared;
using FutureValue.Persistence.EfImplementationTests;
using FutureValue.Domain.Entities;

namespace FutureValue.Persistence.EfImplementation.AspUsers.Tests
{
    public class AspUserRepositoryTests : IClassFixture<DatabaseFixture>
    {
        DatabaseFixture dbFixture;
        public FutureValueContext webContext { get { return dbFixture.webContext; } }
        public AspUserRepositoryTests(DatabaseFixture fixture)
        {
            this.dbFixture = fixture;
        }
        [Fact()]
        public void AspUserRepositoryTest()
        {
            AspUserRepository aspUserRepository = new AspUserRepository(webContext);
            Assert.True(aspUserRepository.GetAll().Count()>0);
        }

        [Fact()]
        public void AddTest()
        {
            AspUserRepository aspUserRepository = new AspUserRepository(webContext);
            int countBefore = aspUserRepository.GetAll().Count();
            aspUserRepository.Register(new AspUser
            {
                ID = 2,
                IsActive = true,
                UserName = "Sample2",
                UserPassword = ""
            }, "new password");
            aspUserRepository.SaveChanges();
            Assert.True(aspUserRepository.GetAll().Count() > countBefore);
        }

        [Fact()]
        public void FindTest()
        {
            AspUserRepository aspUserRepository = new AspUserRepository(webContext);
            Assert.NotNull(aspUserRepository.Find("Sample"));
        }

        [Fact()]
        public void RegisterTest()
        {
            AspUserRepository aspUserRepository = new AspUserRepository(webContext);
            int countBefore = aspUserRepository.GetAll().Count();
            var addeduser = new AspUser
            {
                ID = 2,
                IsActive = true,
                UserName = "Sample2",
                UserPassword = ""
            };
            aspUserRepository.Register(addeduser, "new password");
            aspUserRepository.SaveChanges();
            Assert.True(aspUserRepository.GetAll().Count() > countBefore);
            Assert.NotEqual("", addeduser.UserPassword);
        }

        [Fact()]
        public void FindTestWithPassword()
        {
            AspUserRepository aspUserRepository = new AspUserRepository(webContext);
            Assert.NotNull(aspUserRepository.Find("Sample",dbFixture.defaultPassword));
        }
    }
}