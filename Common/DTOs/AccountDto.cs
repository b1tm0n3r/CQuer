using Common.DataModels.IdentityManagement;

namespace Common.DTOs
{
    public class AccountDto
    {
        public int AccountId { get; set; }
        public string Username { get; set; }
        public AccountType AccountType { get; set; }
    }
}