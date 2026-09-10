using System.ComponentModel.DataAnnotations;

namespace EComerceAPI.DTOs
{
    public class CreateProductDto
    {
        [Required]
        [StringLength(100, MinimumLength =2)]
        public string Name { get; set; }
        [Required]
        [Range(0.01,double.MaxValue)]
        public decimal Price { get; set; }
        [StringLength(500)]
        public string Description  { get; set; }
        [Range(0,int.MaxValue)]
        public int StockQuantity { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        [StringLength(500)]
        public int CategoryId { get; set; }
    }
}
