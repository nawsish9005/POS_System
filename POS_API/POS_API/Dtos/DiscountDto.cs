using System.ComponentModel.DataAnnotations;

namespace POS_API.Dtos
{
    public class DiscountDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(0, 100)]
        public decimal Percentage { get; set; }
        public bool IsActive { get; set; }
    }
}
