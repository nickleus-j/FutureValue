using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FutureValue.Persistence.Shared;
using Microsoft.EntityFrameworkCore;

namespace FutureValue.Persistence.EfImplementation.Shared
{
    public class Repository<T>:IRepository<T> where T : class
    {
        protected DbContext dbContext;
        public IFutureValueContext Context => dbContext != null ? dbContext as IFutureValueContext : new FutureValueContext();

        public Repository(DbContext context)
        {
            this.dbContext = context;
        }

        public virtual T Add(T entity)
        {
            return dbContext.Set<T>()
                .Add(entity)
                .Entity;
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return dbContext.Set<T>()
                .AsQueryable()
                .Where(predicate).ToList();
        }

        public virtual T Get(Guid id)
        {
            return dbContext.Find<T>(id);
        }
        public virtual T Get(int id)
        {
            return dbContext.Find<T>(id);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return dbContext.Set<T>()
                .AsQueryable()
                .ToList();
        }

        public virtual T Update(T entity)
        {
            return dbContext.Update(entity)
                .Entity;
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public IEnumerable<T> GetAll(int page, int maxCount = 100)
        {
            return dbContext.Set<T>()
                .AsQueryable().Skip(page*maxCount).Take(maxCount)
                .ToList();
        }
    }
}
