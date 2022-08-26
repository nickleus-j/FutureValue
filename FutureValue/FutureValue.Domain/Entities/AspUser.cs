using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace FutureValue.Domain.Entities
{
    public class AspUser
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(100)]
        [MinLength(1)]
        public string UserName { get; set; }
        [MinLength(6)]
        public string UserPassword { get; set; }
        public bool IsActive { get; set; }

    }
}
