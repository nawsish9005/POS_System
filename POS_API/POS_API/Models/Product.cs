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
        public DateOnly ExpiryDate { get; set; }

        [Required]
        public DateOnly ManufactureDate { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Photo { get; set; }

        public int Quantity { get; set; }

        public int Stock { get; set; }

        public int UnitPrice { get; set; }

        public int BranchId { get; set; }
        public Branches Branches { get; set; }

        public int CategoryId { get; set; }
        public Categories Categories { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public ICollection<SalesDetails> SalesDetails { get; set; }
        public ICollection<SalesProduct> SalesProducts { get; set; }
    }
}
