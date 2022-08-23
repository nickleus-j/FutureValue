using FutureValue.Domain;
using FutureValue.Persistence.ProjectionForms;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutureValue.Persistence.Shared
{
    public interface IUnitOfWork
    {
        public IProjectionFormRepository ProjectionFormRepository { get; }
        
        void Save();
    }
}
