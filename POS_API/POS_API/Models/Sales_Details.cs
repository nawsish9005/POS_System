using System.ComponentModel.DataAnnotations;

namespace POS_API.Models
{
    public class Sales_Details
    {
        public int Id { get; set; }

        [Range(0, 100)]
        public float Discount { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue)]
        public float Total_Price { get; set; }

        [Range(0, double.MaxValue)]
        public float Unit_Price { get; set; }

        public int Product_Id { get; set; }

        public int Sales_Id { get; set; }
    }
}
