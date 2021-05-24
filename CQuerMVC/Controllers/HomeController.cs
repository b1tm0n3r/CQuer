using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Common.DataModels.IdentityManagement;
using Common.DTOs;
using CommonServices.ClientService.AccountClient;
using CQuerMVC.Helpers;
using CQuerMVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CQuerMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountClientService _clientService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IAccountClientService clientService, ILogger<HomeController> logger)
        {
            _clientService = clientService;
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return View();

            var response = await _clientService.LoginResponse(loginDto);
            if (response.IsSuccessful)
            {
                var signInUser = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDto>(response.Content);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, GeneratePrincipal.GetPrincipal(signInUser));
                return RedirectToAction(signInUser.AccountType==AccountType.StandardUser ? "UserPanel" : "AdminPanel", "Account");
            }
            ModelState.AddModelError(nameof(LoginDto.Password),"Invalid username or password!");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterStandardUserViewModel registerDto)
        {
            if (!ModelState.IsValid)
                return View();
            
            var registerResponse = await _clientService.RegisterResponse(registerDto);
            if (registerResponse.IsSuccessful)
            {
                var location = _clientService.GetUserLocation(registerResponse);
                var id = new string(location.Where(Char.IsDigit).ToArray());
                var signInUser = _clientService.GetUserDtoById(Int32.Parse(id));
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, GeneratePrincipal.GetPrincipal(signInUser.Result));
                
                return RedirectToAction("UserPanel", "Account");
            }
            ModelState.AddModelError(nameof(RegisterStandardUserViewModel.Username), registerResponse.Content.Trim('"'));

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}