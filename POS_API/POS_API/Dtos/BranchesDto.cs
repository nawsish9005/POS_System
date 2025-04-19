using System.ComponentModel.DataAnnotations;

namespace POS_API.Dtos
{
    public class BranchesDto
    {
        public int Id { get; set; }

        [Required]
        public string BranchName { get; set; }

        [Required]
        public string Location { get; set; }
    }
}
