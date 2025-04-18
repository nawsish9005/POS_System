﻿namespace POS_API.Models
{
    public class PurchaseItem
    {
        public int Id { get; set; }
        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
    }
}
