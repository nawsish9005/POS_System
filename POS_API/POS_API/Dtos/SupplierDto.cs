using System.ComponentModel.DataAnnotations;

namespace POS_API.Dtos
{
    public class SupplierDto
    {
        public int Id { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string ContactName { get; set; }

        [Required, Phone]
        public string Phone { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public string Address { get; set; }
    }
}
