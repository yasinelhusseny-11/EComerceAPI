using EComerceAPI.Models;

namespace EComerceAPI.Repositories
{
    public interface IOrderItemRepository
    {
        IEnumerable<OrderItem> GetAll();
        OrderItem? GetById(int id);
        void Add(OrderItem orderItem);
        void Update(OrderItem orderItem);
        void Delete(OrderItem orderItem);
    }
}
