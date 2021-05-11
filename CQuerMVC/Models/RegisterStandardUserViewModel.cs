using Common.DataModels.IdentityManagement;
using Common.DTOs;

namespace CQuerMVC.Models
{
    public class RegisterStandardUserViewModel : RegisterDto
    {
        public new AccountType AccountType { get; } = AccountType.StandardUser;
    }
}