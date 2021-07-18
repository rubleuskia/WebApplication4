using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models.UserAccount
{
    public class RegisterUserViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}