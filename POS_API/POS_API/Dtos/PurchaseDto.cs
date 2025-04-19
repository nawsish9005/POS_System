using System.ComponentModel.DataAnnotations;

namespace POS_API.Dtos
{
    public class PurchaseDto
    {
        public int Id { get; set; }
        [Required]
        public int SupplierId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<PurchaseItemDto> PurchaseItems { get; set; }
    }
}
