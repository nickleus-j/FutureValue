using Xunit;
using FutureValue.Persistence.EfImplementation.AspUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FutureValue.Domain.Entities;
using FutureValue.Persistence.AspUsers;

namespace FutureValue.Persistence.EfImplementation.AspUsers.Tests
{
    public class UserAuthenticatorTests
    {
        string defaultPssword = "default 123";
        [Fact()]
        public void HashPasswordTest()
        {
            UserAuthenticator authenticator = new UserAuthenticator();
            Assert.NotEqual(defaultPssword, authenticator.HashPassword(defaultPssword));
        }
        [Fact()]
        public void VerifyTest()
        {
            UserAuthenticator authenticator = new UserAuthenticator();
            var au = new AspUser
            {
                IsActive = true,
                UserName = "sample user",
                UserPassword = authenticator.HashPassword(defaultPssword)
            };
            Assert.True(authenticator.VerifyAuthenticatingUser(au, defaultPssword));
        }
    }
}