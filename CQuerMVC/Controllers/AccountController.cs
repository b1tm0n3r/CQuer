using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTOs;
using CommonServices.AccountServices;
using CQuerMVC.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CQuerMVC.Controllers
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
        public async Task<ActionResult> LoginAccount(LoginDto loginDto)
        {
            var account = await _accountService.GetAccountFromLogin(loginDto);

            if (account == null)
                return Unauthorized("Invalid username");
            
            var login = _accountService.Login(loginDto);
            
            if (await login)
                return Ok();
            return Unauthorized("Invalid password");
        }
        
    }
}