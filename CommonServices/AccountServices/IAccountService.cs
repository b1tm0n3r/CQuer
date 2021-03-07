using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DataModels.IdentityManagement;
using Common.DTOs;
using CQuerMVC;
using CQuerMVC.DTOs;

namespace CommonServices.AccountServices
{
    public interface IAccountService
    {
        Task<int> Register(RegisterDto registerDto);
        Task<bool> AccountExists(string username);
        Task<bool> Login(LoginDto loginDto);
        Task<Account> GetAccountFromLogin(LoginDto loginDto);
        Task<IEnumerable<AccountDto>> GetAccounts();
        Task<AccountDto> GetAccount(int id);
        Task<bool> UpdateAccount(int id, UpdateAccountDto accountDto);
    }
}