using System.ComponentModel.DataAnnotations;

namespace EComerceAPI.DTOs
{
    public class CartItemDto
    {
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
