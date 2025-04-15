using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace POS_API.Models
{
    public class Token
    {
        public int Id { get; set; }

        [Range(0, 1)]
        public bool IsLoggedOut { get; set; }

        [Required]
        public string TokenValue { get; set; }

        public long UserId { get; set; }
        [ValidateNever]
        public Users Users { get; set; }
    }
}
