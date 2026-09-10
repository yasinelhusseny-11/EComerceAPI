using EComerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EComerceAPI.Data
{
    public class EComerceContext: DbContext
    {
        public EComerceContext(DbContextOptions<EComerceContext> options) : base(options)
        { 
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
