using Common.DataModels.IdentityManagement;

namespace Common.DTOs
{
    public class UserDto
    {
        public string Username { get; set; }
        public AccountType AccountType { get; set; }
    }
}