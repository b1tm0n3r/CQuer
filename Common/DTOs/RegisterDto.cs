using System.ComponentModel.DataAnnotations;
using Common.DataModels.IdentityManagement;

namespace Common.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$",ErrorMessage = "Please enter up minimum eight characters, at least one letter and one number")] 
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords do not match! Try Again")]
        public string RepeatedPassword { get; set; }
        public AccountType AccountType { get; set; }
    }
}