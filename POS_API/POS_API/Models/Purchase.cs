namespace POS_API.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }

        public Supplier Supplier { get; set; }
        public List<PurchaseItem> Items { get; set; }
    }
}
