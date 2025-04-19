using System.ComponentModel.DataAnnotations;

namespace POS_API.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public IFormFile? Photo { get; set; }

        public int StockQuantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public DateTime ManufactureDate { get; set; }
        public string? PhotoUrl { get; set; }


        public int BranchId { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
    }
}
