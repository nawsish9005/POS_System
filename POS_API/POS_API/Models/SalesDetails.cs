using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_API.Models
{
    public class SalesDetails
    {
        public int Id { get; set; }

        [Range(0, 100)]
        public float Discount { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue)]
        public float Total_Price { get; set; }

        [Range(0, double.MaxValue)]
        public float Unit_Price { get; set; }

        public int ProductId { get; set; }

        [ValidateNever]
        public Product Product { get; set; }

        public int SalesId { get; set; }

        [ValidateNever]
        public Sales Sales { get; set; }
    }
}
