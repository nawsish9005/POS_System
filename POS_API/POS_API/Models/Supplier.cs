using System.ComponentModel.DataAnnotations;

namespace POS_API.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        [Phone]
        public string Cell { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
