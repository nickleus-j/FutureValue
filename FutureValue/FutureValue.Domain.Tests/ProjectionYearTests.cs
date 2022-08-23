using FutureValue.Domain;

namespace FutureValue.Domain.Tests
{
    public class ProjectionYearTests
    {
        [Fact]
        public void FutureValue_TestCalculation()
        {
            var projection = new ProjectionYear()
            {
                InterestRate = 10,
                StartValue = 1000,
                Year = 1
            };
            Assert.Equal(1100, projection.FutureValue);
        }
    }
}
