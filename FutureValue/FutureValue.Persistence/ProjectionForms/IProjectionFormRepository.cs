using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FutureValue.Domain.Entities;
using FutureValue.Persistence.Shared;

namespace FutureValue.Persistence.ProjectionForms
{
    public interface IProjectionFormRepository:IRepository<ProjectionForm>
    {
        public IEnumerable<ProjectionForm> GetForms(DateTimeOffset startDate, DateTimeOffset? endDate, int page = 1, int pageSize = 10);
        public IEnumerable<ProjectionForm> GetAll(int userId);
        public bool Delete(int id);
    }
}
