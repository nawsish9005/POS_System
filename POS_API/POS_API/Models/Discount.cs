namespace POS_API.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Percentage { get; set; }
        public bool IsActive { get; set; }
    }
}
