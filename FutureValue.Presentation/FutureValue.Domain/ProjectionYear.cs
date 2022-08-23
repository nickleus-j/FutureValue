using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutureValue.Domain
{
    public class ProjectionYear
    {
        public int Year { get; set; }
        public decimal StartValue { get;set; }
        public decimal InterestRate { get; set; }
        public decimal FutureValue => StartValue + (StartValue * (InterestRate / 100));
    }
}
