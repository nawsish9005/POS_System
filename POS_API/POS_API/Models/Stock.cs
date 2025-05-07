namespace POS_API.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int BranchesId { get; set; }
        public Branches Branches { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<PurchaseItem> PurchaseItems { get; set; }
    }
}
