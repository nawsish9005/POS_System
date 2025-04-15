using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_API.Models
{
    public class SalesProduct
    {
        public int SalesId { get; set; }
        public Sales Sales { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
