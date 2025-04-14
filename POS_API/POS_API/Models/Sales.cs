using System.ComponentModel.DataAnnotations;

namespace POS_API.Models
{
    public class Sales
    {
        public int Id { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0.0, double.MaxValue)]
        public decimal TotalPrice { get; set; }

        [Required]
        public DateTime SalesDate { get; set; }

        [Range(0, 100)]
        public float Discount { get; set; }

        public int SalesDetailsId { get; set; }
    }
}
