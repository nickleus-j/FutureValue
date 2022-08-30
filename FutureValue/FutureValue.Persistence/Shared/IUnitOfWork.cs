using FutureValue.Domain;
using FutureValue.Persistence.ProjectionForms;
using FutureValue.Persistence.AspUsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutureValue.Persistence.Shared
{
    public interface IUnitOfWork
    {
        public IProjectionFormRepository ProjectionFormRepository { get; }
        public IAspUserRepository AspUserRepository { get; }
        void Save();
    }
}
