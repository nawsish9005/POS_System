namespace POS_API.Models
{
    public class Tax
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; } // e.g., 15 for 15%
    }
}
