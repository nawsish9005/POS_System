using System.ComponentModel.DataAnnotations;

namespace POS_API.Dtos
{
    public class StockDto
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int BranchesId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }
    }

}
