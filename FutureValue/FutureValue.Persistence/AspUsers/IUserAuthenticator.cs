using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FutureValue.Domain;
using FutureValue.Domain.Entities;

namespace FutureValue.Persistence.AspUsers
{
    public interface IUserAuthenticator
    {
        bool VerifyAuthenticatingUser(AspUser user, string unhashedPassword);
        string HashPassword(string password);
    }
}
