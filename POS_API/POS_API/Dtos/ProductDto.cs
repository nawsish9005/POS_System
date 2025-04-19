namespace POS_API.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string? Photo { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime ManufactureDate { get; set; }

        public int BranchId { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
    }
}
