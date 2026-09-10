using System.Security.Principal;

namespace EComerceAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public Cart? Cart { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
