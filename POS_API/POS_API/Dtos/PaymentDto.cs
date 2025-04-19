using System.ComponentModel.DataAnnotations;

namespace POS_API.Dtos
{
    public class PaymentDto
    {
        public int Id { get; set; }
        [Required]
        public int SaleId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
    }
}
