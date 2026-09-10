using EComerceAPI.Data;
using EComerceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EComerceAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly EComerceContext _context;
        public ProductRepository(EComerceContext context)
        {
            _context = context;
        }
        public void Add(Product product)
        {
           _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public List<Product> GetAll()
        {
            return _context.Products
        .Include(p => p.Category)
        .ToList();
        }

        public Product GetById(int id)
        {
            return _context.Products
        .Include(p => p.Category)
        .FirstOrDefault(p => p.Id == id);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }
}
