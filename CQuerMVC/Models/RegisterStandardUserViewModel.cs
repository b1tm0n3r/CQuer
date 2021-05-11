using Common.DataModels.IdentityManagement;
using Common.DTOs;

namespace CQuerMVC.Models
{
    public class RegisterStandardUserViewModel : RegisterDto
    {
        public AccountType AccountType { get; set; } = AccountType.StandardUser;
    }
}