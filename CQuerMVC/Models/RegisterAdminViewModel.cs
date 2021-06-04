using Common.DataModels.IdentityManagement;
using Common.DTOs;

namespace CQuerMVC.Models
{
    public class RegisterAdminViewModel : RegisterDto
    {
        public new AccountType AccountType { get; set; } = AccountType.Administrator;
    }
}