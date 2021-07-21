using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Range(1, 100)]
        public int? Age { get; set; }
    }
}
