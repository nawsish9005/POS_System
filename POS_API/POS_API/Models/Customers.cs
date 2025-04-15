using System.ComponentModel.DataAnnotations;

namespace POS_API.Models
{
    public class Customers
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required, Phone]
        public string Cell { get; set; }
        public ICollection<Sales> Sales { get; set; }
    }
}
