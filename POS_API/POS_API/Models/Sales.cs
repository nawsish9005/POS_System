using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_API.Models
{
    public class Sales
    {
        public int Id { get; set; }

        [Required]
        public string CustomerName { get; set; }

        public float Discount { get; set; }

        public int Quantity { get; set; }

        public DateOnly SalesDate { get; set; }

        public decimal TotalPrice { get; set; }

        public int CustomerId { get; set; }
        public Customers Customers { get; set; }
        public ICollection<SalesDetails> SalesDetails { get; set; }
        public ICollection<SalesProduct> SalesProducts { get; set; }
    }
}
