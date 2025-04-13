namespace POS_API.Model
{
    public class Sales_Details
    {
        public int Id { get; set; }
        public float Discount { get; set; }
        public int Quantity { get; set; }
        public float Total_Price { get; set; }
        public float Unit_Price { get; set; }
        public int Product_Id { get; set; }
        public int Sales_Id { get; set; }
    }
}
