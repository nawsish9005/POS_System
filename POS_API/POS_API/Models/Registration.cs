using Microsoft.EntityFrameworkCore;

namespace POS_API.Models
{
    [Keyless]
    public class Registration
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
