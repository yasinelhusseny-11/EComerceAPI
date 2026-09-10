using System.ComponentModel.DataAnnotations;

namespace EComerceAPI.DTOs
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        [Range(1, int.MaxValue)]
        public int OrderId { get; set; }
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        [Range(0.01, double.MaxValue)]
        public decimal UnitPrice { get; set; }
    }
}
