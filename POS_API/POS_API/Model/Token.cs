using System.Numerics;

namespace POS_API.Model
{
    public class Token
    {
        public int Id { get; set; }
        public int IsLoggedOut { get; set; }
        public string TokenName { get; set; }
        public BigInteger UserId { get; set; }
    }
}
