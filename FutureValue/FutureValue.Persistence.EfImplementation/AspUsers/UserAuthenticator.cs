using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using FutureValue.Domain.Entities;
using FutureValue.Persistence.AspUsers;
namespace FutureValue.Persistence.EfImplementation.AspUsers
{
    public class UserAuthenticator : IUserAuthenticator
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyAuthenticatingUser(AspUser user, string unhashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(unhashedPassword,user.UserPassword);
        }
    }
}
