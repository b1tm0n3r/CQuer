using Common.DTOs;
using CommonServices.ClientService;
using Microsoft.AspNetCore.Mvc;

namespace CQuerMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountClientService _client;

        public AccountController(IAccountClientService client)
        {
            _client = client;
        }
        
        public IActionResult Login(LoginDto loginDto)
        {
            var response = _client.LoginResponse(loginDto);
            if (response.Result)
                return RedirectToAction("UserPanel");
            return View(loginDto);
        }

        public IActionResult Register()
        {
            return RedirectToAction("UserPanel");
        }
        
        public IActionResult UserPanel()
        {
            return View();
        }
        
    }
}