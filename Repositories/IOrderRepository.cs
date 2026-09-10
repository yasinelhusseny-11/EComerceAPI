using EComerceAPI.Models;

namespace EComerceAPI.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        Order? GetById(int id);
        void Add(Order order);
        void Update(Order order);
        void Delete(Order order);

        IEnumerable<Order> GetByUserId(int userId);
        Task<Order> CreateOrderFromCartAsync(int userId);

    }
}
