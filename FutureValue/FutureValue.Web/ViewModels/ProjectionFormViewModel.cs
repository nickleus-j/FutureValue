using System.ComponentModel.DataAnnotations;

namespace FutureValue.Web.ViewModels
{
    public class ProjectionFormViewModel
    {
        public int ID { get; set; }
        [Range(0.0, 1e12)]
        [Display(Name = "Preset Value", AutoGenerateFilter = false)]
        public decimal PresetValue { get; set; }
        [MinLength(1)]
        [MaxLength(100)]
        public string Name { get; set; }
        [Range(0.0, 1e9)]
        [Display(Name = "Lower Bound Interest", AutoGenerateFilter = false)]
        public decimal LowerBoundInterest { get; set; }
        [Range(0.0, 1e9)]
        [Display(Name = "Upper Bound Interest", AutoGenerateFilter = false)]
        public decimal UpperBoundInterest { get; set; }
        [Range(0.0,1e9)]
        [Display(Name = "Incremental Rate", AutoGenerateFilter = false)]
        public decimal IncrementalRate { get; set; }
        [Range(1, int.MaxValue)]
        [Display(Name = "Maturity Years", AutoGenerateFilter = false)]
        public int MaturityYears { get; set; }
        //public int? AspUserId { get; set; }
        [Display(Name = "Date Created", AutoGenerateFilter = false)]
        public DateTimeOffset? DateCreated { get; set; }

        public IEnumerable<ProjectionYearViewModel>? Projections { get; set; }
    }
}
