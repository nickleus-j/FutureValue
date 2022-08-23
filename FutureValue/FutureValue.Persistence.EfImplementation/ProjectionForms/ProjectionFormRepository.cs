using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FutureValue.Persistence.Shared;
using FutureValue.Persistence.ProjectionForms;
using FutureValue.Domain;
using FutureValue.Persistence.EfImplementation.Shared;
using Microsoft.EntityFrameworkCore;

namespace FutureValue.Persistence.EfImplementation.ProjectionForms
{
    public class ProjectionFormRepository : Repository<ProjectionForm>, IProjectionFormRepository
    {
        public ProjectionFormRepository(FutureValueContext context) : base(context as DbContext)
        {

        }
        public override IEnumerable<ProjectionForm> GetAll()
        {
            return Context.ProjectionForm.Where(f => f.IsActive);
        }
        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                Context.ProjectionForm.Single(f => f.ID == id).IsActive = false;
                result = true;
            }catch(Exception ex) { }
            return result;
        }

        public IEnumerable<ProjectionForm> GetForms(DateTimeOffset startDate, DateTimeOffset? endDate, int page = 1, int pageSize = 10)
        {
            //if (endDate.HasValue)
            //{
            //    return Context.ProjectionForm.Where(form=>form.DateCreated >= startDate && form.DateCreated <= endDate)
            //        .Skip((page-1)*pageSize).Take(pageSize).ToList();
            //}
            return Context.ProjectionForm.Where(form => form.DateCreated >= startDate&&(!endDate.HasValue|| form.DateCreated <= endDate.Value))
                   .Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        
    }
}
