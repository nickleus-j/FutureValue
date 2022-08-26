using FutureValue.Domain;
using System.ComponentModel.DataAnnotations;
using FutureValue.Domain.Attributes;
namespace FutureValue.WebApi.DTO
{
    public class ProjectionFormDto
    {
        public int ID { get; set; }
        [Range(0.0, 1e12)]
        public decimal PresetValue { get; set; }
        [MinLength(1)]
        [MaxLength(100)]
        public string Name { get; set; }
        [Range(0.0, 1e9)]
        [NumericLessThan("UpperBoundInterest", AllowEquality = true)]
        public decimal LowerBoundInterest { get; set; }
        [Range(0.0, 1e9)]
        public decimal UpperBoundInterest { get; set; }
        [Range(0.0,1e9)]
        public decimal IncrementalRate { get; set; }
        [Range(1, int.MaxValue)]
        public int MaturityYears { get; set; }
        public int? AspUserId { get; set; }
        public DateTimeOffset? DateCreated { get; set; }
        public IEnumerable<ProjectionYear>? Projections { get; set; }
    }
}
