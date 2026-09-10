using EComerceAPI.Data;
using EComerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EComerceAPI.Repositories
{


    public class CategoryRepository : ICategoryRepository
    {
        private readonly EComerceContext _context;

        public CategoryRepository(EComerceContext context)
        {
            _context = context;
        }
        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }
        public Category? GetById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }
        public void Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        public void Update(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }
        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
    
}
