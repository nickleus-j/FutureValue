using System;
using System.Collections.Generic;
using System.Text;
using FutureValue.Persistence.Shared;
using FutureValue.Persistence.ProjectionForms;
using FutureValue.Persistence.EfImplementation.ProjectionForms;
using Microsoft.EntityFrameworkCore;

namespace FutureValue.Persistence.EfImplementation.Shared
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FutureValueContext context;

        public UnitOfWork(FutureValueContext database)
        {
            context = database;
        }
        public UnitOfWork()
        {
            context = new FutureValueContext();
        }
        public IProjectionFormRepository _ProjectionFormRepository { get; set; }
        public IProjectionFormRepository ProjectionFormRepository
        {
            get
            {
                if (_ProjectionFormRepository == null)
                {
                    _ProjectionFormRepository = new ProjectionFormRepository(context);
                }

                return _ProjectionFormRepository;
            }
        }
        public void Save(){ context.Save();}
        

    }
}
