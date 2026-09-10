using EComerceAPI.Data;
using EComerceAPI.Models;

namespace EComerceAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EComerceContext _context;

        public UserRepository(EComerceContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User? GetById(int id)
        {
            return _context.Users
                .FirstOrDefault(u => u.Id == id);
        }
        public User? GetByEmail(string email)
        {
            return _context.Users
                .FirstOrDefault(u => u.Email == email);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
