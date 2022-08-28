using Xunit;
using FutureValue.Domain.Entities;
using FutureValue.Persistence.EfImplementation.Shared;
using FutureValue.Persistence.EfImplementation.ProjectionForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FutureValue.Persistence.EfImplementationTests;

namespace FutureValue.Persistence.EfImplementation.ProjectionForms.Tests
{
    public class ProjectionFormRepositoryTests : IClassFixture<DatabaseFixture>
    {
        DatabaseFixture dbFixture;
        public FutureValueContext webContext { get { return dbFixture.webContext; } }
        public ProjectionFormRepositoryTests(DatabaseFixture fixture)
        {
            this.dbFixture = fixture;
        }
        [Fact()]
        public void ProjectionFormRepositoryTest()
        {
            ProjectionFormRepository repo=new ProjectionFormRepository(webContext);
            Assert.NotNull(repo.GetAll());
        }

        [Fact()]
        public void AddTest()
        {
            ProjectionFormRepository repo = new ProjectionFormRepository(webContext);
            int id = repo.GetAll().Last().ID,prevCount=repo.GetAll().Count();
            repo.Add(new ProjectionForm
            {
                IncrementalRate = 5,
                LowerBoundInterest = 5,
                UpperBoundInterest = 25,
                IsActive = true,
                MaturityYears = 5,
                Name = "Look out for variable Interest rates",
                DateCreated = DateTime.Now,
                PresetValue = 15000
            });
            repo.SaveChanges();
            Assert.NotEqual(id, repo.GetAll().Last().ID);
            Assert.NotEqual(prevCount, repo.GetAll().Count());
        }

        [Fact()]
        public void UpdateTest()
        {
            ProjectionFormRepository repo = new ProjectionFormRepository(webContext);
            var form = repo.GetForms(DateTimeOffset.Now.Add(TimeSpan.FromDays(-1)), DateTimeOffset.Now).Last();
            int id = form.ID;
            string origName=repo.GetAll().Last().Name;
            form.Name = "Test Change";
            repo.Update(form);
            repo.SaveChanges();
            Assert.NotEqual(origName, repo.GetForms(DateTimeOffset.Now.Add(TimeSpan.FromDays(-1)), DateTimeOffset.Now).Last().Name);
        }

        [Fact()]
        public void GetAllTest()
        {
            ProjectionFormRepository repo = new ProjectionFormRepository(webContext);
            Assert.True(repo.GetAll().Count() > 1);
        }

        [Fact()]
        public void DeleteTest()
        {
            //setup
            ProjectionFormRepository repo = new ProjectionFormRepository(webContext);
           
            repo.Add(new ProjectionForm
            {
                IncrementalRate = 5,
                LowerBoundInterest = 5,
                UpperBoundInterest = 25,
                IsActive = true,
                MaturityYears = 5,
                Name = "Look out for variable Interest rates",
                DateCreated = DateTime.Now,
                PresetValue = 15000
            });
            repo.SaveChanges();
            int id = repo.GetAll().Last().ID, prevCount = repo.GetAll().Count();
            //Delete test
            repo.Delete(id);
            repo.SaveChanges();
            Assert.NotEqual(id, repo.GetAll().Last().ID);
            Assert.NotEqual(prevCount, repo.GetAll().Count());
        }

        [Fact()]
        public void GetFormsTest()
        {
            ProjectionFormRepository repo = new ProjectionFormRepository(webContext);
            var list = repo.GetForms(DateTimeOffset.Now.Add(TimeSpan.FromDays(-1)), DateTimeOffset.Now);
            Assert.True(list.First().IsActive);
            Assert.True(list.Last().IsActive);
        }
    }
}