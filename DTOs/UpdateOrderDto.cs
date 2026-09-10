using System.ComponentModel.DataAnnotations;

namespace EComerceAPI.DTOs
{
    public class UpdateOrderDto
    {
        [Required]
        public string Status { get; set; }
    }
}
