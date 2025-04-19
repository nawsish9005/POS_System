namespace POS_API.Dtos
{
    public class SaleDto
    {
        public string UserId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal ChangeAmount { get; set; }

        public List<SaleItemDto> Items { get; set; }
    }
}
