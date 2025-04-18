using Microsoft.EntityFrameworkCore;

namespace POS_API.Models
{
    [Keyless]
    public class UserRole
    {
        public string UserName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
