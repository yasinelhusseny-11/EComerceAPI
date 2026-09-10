using System.ComponentModel.DataAnnotations;

namespace EComerceAPI.DTOs
{
    public class UpdateCategoryDto
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
    }
}
