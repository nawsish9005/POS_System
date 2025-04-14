using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_API.Models
{
    public class SalesProduct
    {
        public int Id { get; set; }

        [Required]
        public int SalesId { get; set; }
        [ForeignKey("SalesId")]
        [ValidateNever]
        public Sales Sales { get; set; }
        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product Product { get; set; }
    }
}
