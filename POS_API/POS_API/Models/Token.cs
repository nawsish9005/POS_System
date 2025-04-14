using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace POS_API.Models
{
    public class Token
    {
        public int Id { get; set; }

        [Range(0, 1)]
        public int IsLoggedOut { get; set; }

        [Required]
        public string TokenName { get; set; }

        public BigInteger UserId { get; set; }
    }
}
