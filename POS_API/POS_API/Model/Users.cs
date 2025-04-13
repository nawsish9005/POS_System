namespace POS_API.Model
{
    public class Users
    {
        public int Id { get; set; }
        public int Active { get; set; }
        public string Address { get; set; }
        public string Cell { get; set; }
        public DateOnly DOB { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public string Password { get; set; }
    }
}
