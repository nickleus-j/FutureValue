using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace FutureValue.Persistence.Shared
{
    public interface IRepository<T>
    {
        T Add(T entity);
        T Update(T entity);
        T Get(Guid id);
        T Get(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(int page,int maxCount=100);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void SaveChanges();
    }
}
