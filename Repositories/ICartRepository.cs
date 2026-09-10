using EComerceAPI.Models;

namespace EComerceAPI.Repositories
{
    public interface ICartRepository
    {
        IEnumerable<Cart> GetAll();
        Cart? GetById(int id);
        Cart? GetByUserId(int userId);
        CartItem? GetItemById(int id, int userId);
        Task AddItemAsync(int userId, int productId, int quantity);
        void AddItem(CartItem cartItem);
        void RemoveItem(CartItem cartItem);
        void UpdateQuantity(CartItem cartItem);
        CartItem GetItemById(int id);
    }
}
