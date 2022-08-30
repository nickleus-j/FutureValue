using System;
using System.Collections.Generic;
using System.Text;
using FutureValue.Persistence.Shared;
using FutureValue.Persistence.ProjectionForms;
using FutureValue.Persistence.EfImplementation.AspUsers;
using FutureValue.Persistence.EfImplementation.ProjectionForms;
using Microsoft.EntityFrameworkCore;
using FutureValue.Persistence.AspUsers;

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
        private IProjectionFormRepository _ProjectionFormRepository { get; set; }
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
        private IAspUserRepository _AspUserRepository { get; set; }

        public IAspUserRepository AspUserRepository
        {
            get
            {
                if (_AspUserRepository == null)
                {
                    _AspUserRepository = new AspUserRepository(context);
                }

                return _AspUserRepository;
            }
        }

        public void Save(){ context.Save();}
        

    }
}
