using EComerceAPI.Models;

namespace EComerceAPI.Repositories
{
    public interface ICategoryRepository
    {
       public IEnumerable<Category> GetAll();
        Category? GetById(int id);
        void Add(Category category);
        void Update(Category category);
        void Delete(Category category);
    }
}
