using System.ComponentModel.DataAnnotations;

namespace FutureValue.WebApi.DTO
{
    public class AspUserDto
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(100)]
        [MinLength(1)]
        public string UserName { get; set; }
        [MinLength(6)]
        public string? UnhashedPassword { get; set; }
    }
}
