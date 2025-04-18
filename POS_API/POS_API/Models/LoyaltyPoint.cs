namespace POS_API.Models
{
    public class LoyaltyPoint
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int Points { get; set; }
        public DateTime EarnedDate { get; set; }
        //If you're building customer loyalty features.
    }
}
