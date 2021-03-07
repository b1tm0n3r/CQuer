using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Common.DataModels.IdentityManagement;
using Common.DTOs;
using CommonServices.AccountServices;
using CQuerMVC;
using CQuerMVC.DTOs;

namespace CommonServicesTests
{
    public class AccountServiceFake : IAccountService
    {
        private readonly List<RegisterDto> _list;
        public AccountServiceFake()
        {
            _list = new List<RegisterDto>
            {
                new (){Username = "user",Password = "password"}
            };
        }
        public async Task<int> Register(RegisterDto registerDto)
        {
            using var hmac = new HMACSHA512();
            
            var account = new Account
            {
                Name = registerDto.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key,
                AccountType = registerDto.AccountType
            };

            return account.AccountId;
        }
        public async Task<bool> AccountExists(string username)
        {
            return _list.Any(x => x.Username == username);
        }

        public async Task<bool> Login(LoginDto loginDto)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Account> GetAccountFromLogin(LoginDto loginDto)
        {
            throw new System.NotImplementedException();

        }
        public async Task<IEnumerable<AccountDto>> GetAccounts()
        {
            throw new System.NotImplementedException();
        }

        public async Task<AccountDto> GetAccount(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UpdateAccount(int id, UpdateAccountDto accountDto)
        {
            throw new System.NotImplementedException();

        }
    }
}