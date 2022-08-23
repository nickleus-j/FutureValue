using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace FutureValue.Domain
{
    public class ProjectionForm
    {
        public int ID { get;set; }
        [Range(0.0,double.MaxValue)]
        public decimal PresetValue { get; set; }
        [MinLength(1)]
        [MaxLength(100)]
        public string Name { get; set; }
        [Range(0.0, double.MaxValue)]
        public decimal LowerBoundInterest { get; set; }
        [Range(0.0, double.MaxValue)]
        public decimal UpperBoundInterest { get; set; }
        [Range(0.0, double.MaxValue)]
        public decimal IncrementalRate { get;set;}
        [Range(1, int.MaxValue)]
        public int MaturityYears { get; set; }
        public int? AspUserId { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset? DateCreated { get; set; }
    }
}
