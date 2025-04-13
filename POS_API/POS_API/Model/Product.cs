namespace POS_API.Model
{
    public class Product
    {
        public int Id { get; set; }
        public DateOnly Expiry_Date { get; set; }
        public DateOnly Manufacture_Date { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public int Quantity { get; set; }
        public int Stock { get; set; }
        public int UnitPrice { get; set; }
        public int BranchId { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
    }
}
