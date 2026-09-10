namespace EComerceAPI.DTOs
{
    public class CartDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ICollection<CartItemResponseDto> CartItems { get; set; }
    }
}
