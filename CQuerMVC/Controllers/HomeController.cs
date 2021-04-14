using System.Diagnostics;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Common.DTOs;
using CommonServices.ClientService;
using CQuerMVC.Helpers;
using CQuerMVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
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
        
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid) return View();
            var response = _clientService.LoginResponse(loginDto);
            var signInUser = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDto>(response.Result.Content);
            if (!response.Result.IsSuccessful) return View(loginDto);
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, GeneratePrincipal.GetPrincipal(signInUser));
            return RedirectToAction("UserPanel","Account");
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}