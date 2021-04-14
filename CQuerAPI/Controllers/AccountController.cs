using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTOs;
using CommonServices.AccountServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CQuerAPI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetAllAccounts()
        {
            var accounts = await _accountService.GetAccounts();
            return accounts.ToList();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDto>> GetAccount(int id)
        {
            var account = await _accountService.GetAccount(id);
            if (account == null)
                return NotFound("Account with given id does not exist");
            return Ok(account);
        }
        
        [HttpPost("register")]
        public async Task<ActionResult> RegisterAccount(RegisterDto registerDto)
        {
            if (await _accountService.AccountExists(registerDto.Username))
            {
                return BadRequest("Username is taken");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var id = await _accountService.Register(registerDto);

            return Created($"/api/account/{id}",null);
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> LoginAccount(LoginDto loginDto)
        {
            var account = await _accountService.GetAccountFromLogin(loginDto);

            if (account == null)
                return Unauthorized("Invalid username");
            
            var login = _accountService.Login(loginDto);

            if (await login)
                return new UserDto
                {
                    Username = account.Name,
                    AccountType = account.AccountType
                };
            return Unauthorized("Invalid password");
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<AccountDto>> UpdateAccount([FromRoute]int id, [FromBody]UpdateAccountDto accountDto)
        {
            if (!ModelState.IsValid)
                BadRequest(ModelState);
            
            var account = await _accountService.UpdateAccount(id, accountDto);
            if (!account)
                return NotFound();
            return Ok();
        }
        
    }
}