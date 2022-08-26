using FutureValue.Domain.Entities;

namespace FutureValue.Domain.Tests
{
    public class ProjectionListerTests
    {
        private ProjectionForm makeTestForm(decimal incrementalRate)
        {
            ProjectionForm form = new ProjectionForm();
            form.PresetValue = 1000;
            form.MaturityYears = 4;
            form.DateCreated = DateTime.UtcNow;
            form.LowerBoundInterest = 10;
            form.UpperBoundInterest = 50;
            form.IncrementalRate = incrementalRate;
            form.IsActive = true;
            return form;
        }
        [Fact]
        public void DecideOnIncrementedInterestRate_Test()
        {
            ProjectionLister lister = new ProjectionLister();
            var form = makeTestForm(10);
            decimal expected = 20;
            decimal result = lister.DecideOnIncrementedInterestRate(form, 10);
            Assert.Equal(expected, result);
            Assert.Equal(10, lister.DecideOnIncrementedInterestRate(form, 0));
            Assert.Equal(30, lister.DecideOnIncrementedInterestRate(form, 20));
        }
        [Fact]
        public void DecideOnIncrementedInterestRate_Test20InterestRate()
        {
            ProjectionLister lister = new ProjectionLister();
            var form = makeTestForm(20);
            decimal expected = 30;
            decimal result = lister.DecideOnIncrementedInterestRate(form, 10);
            Assert.Equal(expected, result);
            Assert.Equal(50, lister.DecideOnIncrementedInterestRate(form, 30));
            Assert.Equal(50, lister.DecideOnIncrementedInterestRate(form, 50));
        }
        [Fact]
        public void DecideOnIncrementedInterestRate_Test0LowerBound()
        {
            ProjectionLister lister = new ProjectionLister();
            var form = makeTestForm(10);
            form.LowerBoundInterest = 0;
            decimal expected = 10;
            decimal result = lister.DecideOnIncrementedInterestRate(form, 0);
            Assert.Equal(expected, result);
        }
        [Fact]
        public void GenerateProjections_Test20Increment()
        {
            ProjectionLister lister = new ProjectionLister();
            var form = makeTestForm(20);
            IEnumerable<ProjectionYear> result =lister.GenerateProjections(form);
            Assert.Equal(4, result.Count());
            Assert.Equal(form.PresetValue, result.First().StartValue);
            Assert.Equal(result.ElementAt(0).InterestRate+ form.IncrementalRate, result.ElementAt(1).InterestRate);
            Assert.Equal(result.ElementAt(1).Year+1, result.ElementAt(2).Year);
            Assert.Equal(result.ElementAt(1).FutureValue, result.ElementAt(2).StartValue);
            Assert.Equal(result.ElementAt(2).InterestRate, result.ElementAt(3).InterestRate);
            Assert.Equal(form.UpperBoundInterest, result.ElementAt(3).InterestRate);
            Assert.Equal((decimal)2145, result.ElementAt(3).StartValue);
            Assert.Equal((decimal)3217.5, result.ElementAt(3).FutureValue);
        }
    }
}