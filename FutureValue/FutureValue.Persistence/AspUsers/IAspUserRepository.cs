using FutureValue.Domain.Entities;
using FutureValue.Persistence.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutureValue.Persistence.AspUsers
{
    public interface IAspUserRepository : IRepository<AspUser>
    {
        public void Register(AspUser user, string unhashedPassword);
        public AspUser? Find(string username);
        public AspUser? Find(string username,string unhashedPassword);
    }
}
