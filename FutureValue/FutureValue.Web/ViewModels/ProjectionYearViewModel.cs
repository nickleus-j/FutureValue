using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FutureValue.Web.ViewModels
{
    public class ProjectionYearViewModel
    {
        [Display(Name = "Year", AutoGenerateFilter = true)]
        public int Year { get; set; }
        [Display(Name = "Start Value", AutoGenerateFilter = true)]
        public decimal StartValue { get;set; }
        [Display(Name = "Interest Rate", AutoGenerateFilter = true)]
        public decimal InterestRate { get; set; }
        [Display(Name = "Future Value", AutoGenerateFilter = true)]
        public decimal FutureValue { get; set; }
    }
}
