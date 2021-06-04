using Common.DTOs;

namespace CQuerMVC.Models
{
    public class LoginViewModel
    {
        public LoginDto LoginDto { get; set; }
        public string RedirectUrl { get; set; }
    }
}
