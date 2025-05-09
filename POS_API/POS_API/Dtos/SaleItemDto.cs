﻿namespace POS_API.Dtos
{
    public class SaleItemDto
    {
        public int Id { get; set; }
        public int SaleId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Subtotal => Quantity * UnitPrice;
    }
}
