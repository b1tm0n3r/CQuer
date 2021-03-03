using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Common.DataModels.IdentityManagement;
using CQuerMVC.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace CQuerMVC.Controllers
{
    public class AccountController : BaseController
    {
        private readonly CQuerDbContext _dbContext;

        public AccountController(CQuerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpPost("register")]
        public async Task<ActionResult<Account>> RegisterAccount(RegisterDto registerDto)
        {
            if (await AccountExists(registerDto.Username))
            {
                return BadRequest("Username is taken");
            }
            using var hmac = new HMACSHA512();

            var account = new Account
            {
                Name = registerDto.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key,
                AccountType = registerDto.AccountType
            };

            _dbContext.Accounts.Add(account);
            await _dbContext.SaveChangesAsync();

            return account;
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<Account>> LoginAccount(LoginDto loginDto)
        {
            var account = await _dbContext.Accounts
                .SingleOrDefaultAsync(x => x.Name == loginDto.Username);

            if (account == null)
                return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(account.PasswordSalt);
            
            var loginHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i=0; i < loginHash.Length; i++)
            {
                if (loginHash[i] != account.PasswordHash[i]) return Unauthorized("Invalid password");
            }
            return account;
        }
        private async Task<bool> AccountExists(string username)
        {
            return await _dbContext.Accounts.AnyAsync(x => x.Name == username);
        }
    }
}