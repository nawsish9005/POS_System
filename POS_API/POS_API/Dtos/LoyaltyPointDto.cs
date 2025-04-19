using System.ComponentModel.DataAnnotations;

namespace POS_API.Dtos
{
    public class LoyaltyPointDto
    {
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public int Points { get; set; }
        public DateTime EarnedDate { get; set; }
    }
}
