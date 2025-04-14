using System.ComponentModel.DataAnnotations;

namespace POS_API.Models
{
    public class SalesProduct
    {
        public int Id { get; set; }

        [Required]
        public int SalesId { get; set; }

        [Required]
        public int ProductId { get; set; }
    }
}
