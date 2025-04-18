using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_API.Models
{
    public class User: IdentityUser
    {
        public string FullName { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}
