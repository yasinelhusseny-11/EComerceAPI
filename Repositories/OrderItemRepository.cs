using EComerceAPI.Data;
using EComerceAPI.Models;

namespace EComerceAPI.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly EComerceContext _context;

        public OrderItemRepository(EComerceContext context)
        {
            _context = context;
        }
        public IEnumerable<OrderItem> GetAll()
        {
            return _context.OrderItems.ToList();
        }
        public OrderItem? GetById(int id)
        {
            return _context.OrderItems
                .FirstOrDefault(oi => oi.Id == id);
        }
        public void Add(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            _context.SaveChanges();
        }
        public void Update(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
            _context.SaveChanges();
        }
        public void Delete(OrderItem orderItem)
        {
            _context.OrderItems.Remove(orderItem);
            _context.SaveChanges();
        }
    }
}
