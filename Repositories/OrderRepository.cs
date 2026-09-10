using EComerceAPI.Data;
using EComerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EComerceAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EComerceContext _context;

        public OrderRepository(EComerceContext context)
        {
            _context = context;
        }
        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.ToList();
        }
        public Order? GetById(int id)
        {
           return _context.Orders.FirstOrDefault(o => o.Id == id);
        }
        public IEnumerable<Order> GetByUserId(int userId)
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.UserId == userId)
                .ToList();
        }
        public async Task<Order> CreateOrderFromCartAsync(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.CartItems.Any())
            {
                throw new Exception("Cart is empty");
            }
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                Status = "Pending",
                TotalAmount = 0
            };
            foreach (var cartItem in cart.CartItems)
            {
                var orderItem = new OrderItem
                {
                    Order = order,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.Product.Price
                };

                order.TotalAmount += cartItem.Quantity * cartItem.Product.Price;

                _context.OrderItems.Add(orderItem);
            }
            _context.Orders.Add(order);

            cart.CartItems.Clear();

            await _context.SaveChangesAsync();

            return order;
        }
        public void Add(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
        public void Update(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }
        public void Delete(Order order)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
    }
}
