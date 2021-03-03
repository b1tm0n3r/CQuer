using System.ComponentModel.DataAnnotations;
using Common.DataModels.IdentityManagement;

namespace CQuerMVC.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public AccountType AccountType { get; set; }
    }
}