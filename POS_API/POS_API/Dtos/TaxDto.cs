using System.ComponentModel.DataAnnotations;

namespace POS_API.Dtos
{
    public class TaxDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0, 100)]
        public decimal Rate { get; set; } // e.g., 15 for 15%
    }
}
