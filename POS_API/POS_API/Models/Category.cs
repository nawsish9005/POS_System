using System.ComponentModel.DataAnnotations;

namespace POS_API.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string CategoryName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
