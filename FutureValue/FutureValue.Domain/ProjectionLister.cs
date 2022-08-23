using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutureValue.Domain
{
    public class ProjectionLister
    {
        public IEnumerable<ProjectionYear> GenerateProjections(ProjectionForm form)
        {
            IList<ProjectionYear> result = new List<ProjectionYear>();
            for(int i = 1; i <= form.MaturityYears; i++)
            {
                var p=new ProjectionYear();
                p.Year=i;
                if (i == 1)
                {
                    p.StartValue = form.PresetValue;
                    p.InterestRate=form.LowerBoundInterest;
                }
                else {
                    ProjectionYear prev = result[i - 2];
                    p.StartValue = prev.FutureValue;
                    p.InterestRate = DecideOnIncrementedInterestRate(form, prev.InterestRate);
                }
                result.Add(p);
            }
            return result;
        }
        public decimal DecideOnIncrementedInterestRate(ProjectionForm form,decimal previousRate)
        {
            decimal incrementedRate=previousRate+form.IncrementalRate;
            return incrementedRate > form.UpperBoundInterest ? form.UpperBoundInterest : incrementedRate;
        }
    }
}
