using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Core.Users
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Range(1, 100)]
        public int? Age { get; set; }

        public IFormFile Photo { get; set; }

        public string PhotoPath { get; set; }

        public string PhotoBase64 { get; set; }
    }
}
