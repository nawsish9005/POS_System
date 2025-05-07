using System.ComponentModel.DataAnnotations;

namespace POS_API.Dtos
{
    public class PurchaseItemDto
    {
        public int Id { get; set; }
        [Required]
        public int StockId { get; set; }
        [Required]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal Subtotal { get; set; }
        //public decimal Subtotal => Quantity * UnitCost;
    }
}
