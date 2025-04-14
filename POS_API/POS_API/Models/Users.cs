using System.ComponentModel.DataAnnotations;

namespace POS_API.Models
{
    public class Users
    {
        public int Id { get; set; }

        [Range(0, 1)]
        public int Active { get; set; }

        public string Address { get; set; }

        [Phone]
        public string Cell { get; set; }

        [Required]
        public DateOnly DOB { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FullName { get; set; }

        public string Gender { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
