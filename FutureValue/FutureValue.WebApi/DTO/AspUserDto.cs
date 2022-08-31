using System.ComponentModel.DataAnnotations;

namespace FutureValue.WebApi.DTO
{
    public class AspUserDto:LoginDto
    {
        [Key]
        public int ID { get; set; }
        
    }
}
