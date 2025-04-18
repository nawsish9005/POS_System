using System.ComponentModel.DataAnnotations;

namespace POS_API.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required, Phone]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
