using System.ComponentModel.DataAnnotations;

namespace POS_API.Models
{
    public class Users
    {
        public long Id { get; set; }

        public bool Active { get; set; }

        public string Address { get; set; }

        public string Cell { get; set; }

        public DateOnly DOB { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public string Image { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public UserRole Role { get; set; }

        public ICollection<Token> Tokens { get; set; }
    }

    public enum UserRole
    {
        Admin,
        Cashier,
        Manager
    }
}
