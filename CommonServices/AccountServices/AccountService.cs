using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common.DataModels.IdentityManagement;
using Common.DTOs;
using CQuerMVC;
using CQuerMVC.DTOs;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace CommonServices.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly ICQuerDbContext _dbContext;
        private readonly IMapper _mapper;

        public AccountService(ICQuerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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
            
            await _dbContext.Accounts.AddAsync(account);
            await _dbContext.SaveChangesAsync();

            return account.AccountId;
        }
        public async Task<bool> AccountExists(string username)
        {
            return await _dbContext.Accounts.AnyAsync(x => x.Name == username);
        }
        public async Task<bool> Login(LoginDto loginDto)
        {
            var account = await GetAccountFromLogin(loginDto);

            using var hmac = new HMACSHA512(account.PasswordSalt);
                
            var loginHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            return !loginHash.Where((t, i) => t != account.PasswordHash[i]).Any();
        }
        public async Task<Account> GetAccountFromLogin(LoginDto loginDto)
        {
            var account = await _dbContext.Accounts
                .SingleOrDefaultAsync(x => x.Name == loginDto.Username);
            return account;
        }

        public async Task<IEnumerable<AccountDto>> GetAccounts()
        {
            var accounts = await _dbContext.Accounts.ToListAsync();
            var accountsDto = _mapper.Map<List<AccountDto>>(accounts);

            return accountsDto;
        }
        
    }
}