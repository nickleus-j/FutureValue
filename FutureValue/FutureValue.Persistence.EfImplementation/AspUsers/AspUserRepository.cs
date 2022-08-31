using FutureValue.Domain.Entities;
using FutureValue.Domain.Exceptions;
using FutureValue.Persistence.AspUsers;
using FutureValue.Persistence.EfImplementation.Shared;
using FutureValue.Persistence.ProjectionForms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutureValue.Persistence.EfImplementation.AspUsers
{
    public class AspUserRepository : Repository<AspUser>, IAspUserRepository
    {
        UserAuthenticator userAuthenticator { get; set; }
        public AspUserRepository(FutureValueContext context) : base(context as DbContext)
        {
            userAuthenticator = new UserAuthenticator();
        }
        public override AspUser Add(AspUser entity)
        {
            if(entity == null || String.IsNullOrEmpty(entity.UserPassword))
            {
                throw new ArgumentNullException("Need password");
            }
            if (Find(entity.UserName) != null)
            {
                throw new DbUpdateException("Name Taken");
            }
            
            return base.Add(entity);
        }
        public AspUser? Find(string username, string unhashedPassword)
        {
            AspUser? user = Context.AspUser.SingleOrDefault(u=>u.UserName.ToLower()==username.ToLower());
            if (user != null)
            {
                user = userAuthenticator.VerifyAuthenticatingUser(user, unhashedPassword) ? user : null;
            }
            return user;
        }

        public void Register(AspUser user, string unhashedPassword)
        {
            user.UserPassword = userAuthenticator.HashPassword(unhashedPassword);
            this.Add(user);
        }

        public AspUser? Find(string username)
        {
            return Context.AspUser.FirstOrDefault(u=>u.UserName.ToLower()==username.ToLower()&&u.IsActive==true);
        }
    }
}
