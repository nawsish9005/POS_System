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
        public DateOnly Expiry_Date { get; set; }

        [Required]
        public DateOnly Manufacture_Date { get; set; }

        [Required]
        public string Name { get; set; }

        public string Photo { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        [Range(0, int.MaxValue)]
        public int UnitPrice { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Categories Category { get; set; }

        [Required]
        public int SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        [ValidateNever]
        public Supplier Supplier { get; set; }
    }
}
