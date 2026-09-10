using EComerceAPI.Data;
using EComerceAPI.DTOs;
using EComerceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EComerceAPI.Repositories
{
    
    public class CartRepository : ICartRepository
    {
        private readonly EComerceContext _context;

        public CartRepository(EComerceContext context)
        {
            _context = context;
        }
        public IEnumerable<Cart> GetAll()
        {
            return _context.Carts.ToList();
        }
        public Cart? GetById(int id)
        {
            return _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefault(c => c.Id == id);
        }
        [HttpGet]
        public Cart? GetByUserId(int userId)
        {
            return _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefault(c => c.UserId == userId);
        }
        public CartItem? GetItemById(int id, int userId)
        {
            return _context.CartItems
                .Include(ci => ci.Cart)
                .FirstOrDefault(ci =>
                    ci.Id == id &&
                    ci.Cart.UserId == userId);
        }
        public void AddItem(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            _context.SaveChanges();
        }
        public void RemoveItem(CartItem cartItem)
        {
            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();
        }
        public void UpdateQuantity(CartItem cartItem)
        {
            _context.CartItems.Update(cartItem);
            _context.SaveChanges();
        }
        public CartItem GetItemById(int id)
        {
            return _context.CartItems.FirstOrDefault(x => x.Id == id);
        }
        public async Task AddItemAsync(int userId, int productId, int quantity)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId
                };

                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            var existingItem = cart.CartItems
                .FirstOrDefault(ci => ci.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = quantity
                };

                cart.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();
        }
    }
}
