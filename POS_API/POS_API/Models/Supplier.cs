using System.ComponentModel.DataAnnotations;

namespace POS_API.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public ICollection<Stock> Stocks { get; set; }
    }
}
