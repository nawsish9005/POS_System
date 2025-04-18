using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace POS_API.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public DateTime ManufactureDate { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Photo { get; set; }

        public int StockQuantity { get; set; }

        public decimal Price { get; set; }

        public int BranchId { get; set; }
        public Branches Branches { get; set; }

        public int CategoryId { get; set; }
        public Category Categories { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public ICollection<SaleItem> SaleItems { get; set; }
        public ICollection<PurchaseItem> PurchaseItems { get; set; }
    }
}
