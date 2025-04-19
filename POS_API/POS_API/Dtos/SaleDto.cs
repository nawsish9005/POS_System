namespace POS_API.Dtos
{
    public class SaleDto
    {
        public int Id { get; set; }
        public DateTime SaleDate { get; set; }

        public int? CustomerId { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal ChangeAmount { get; set; }

        public string UserId { get; set; }

        public List<SaleItemDto> SaleItems { get; set; }
        public List<PaymentDto> Payments { get; set; }
    }
}
