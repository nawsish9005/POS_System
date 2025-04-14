using System.ComponentModel.DataAnnotations;

namespace POS_API.Models
{
    public class Branches
    {
        public int Id { get; set; }

        [Required]
        public string BranchName { get; set; }

        [Required]
        public string Location { get; set; }
    }
}
