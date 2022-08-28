using Xunit;
using FutureValue.Persistence.Shared;
using FutureValue.Persistence.EfImplementation.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FutureValue.Persistence.EfImplementationTests;

namespace FutureValue.Persistence.EfImplementation.Shared.Tests
{
    public class UnitOfWorkTests : IClassFixture<DatabaseFixture>
    {

        DatabaseFixture dbFixture;
        public FutureValueContext webContext { get { return dbFixture.webContext; } }
        public UnitOfWorkTests(DatabaseFixture fixture)
        {
            this.dbFixture = fixture;
        }
        [Fact()]
        public void ProjectionFormRepository_Test()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(webContext);
            Assert.NotNull(unitOfWork.ProjectionFormRepository);
            Assert.Equal(1,unitOfWork.ProjectionFormRepository.Get(1).ID);
        }
    }
}