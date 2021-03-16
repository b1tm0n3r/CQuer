using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using Common.DTOs;
using CQuerMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CQuerMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
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
        
        [HttpPost]
        public IActionResult Login(LoginDto loginDto)
        {
            //TODO: Create correct API client
            HttpClient client = new HttpClient();
                
            var uri = new Uri("https://localhost:6001/api/account/login");
            var json = JsonConvert.SerializeObject(loginDto);
                
            HttpContent stringContent = new StringContent(json, Encoding.UTF8, "application/json"); 
                
            var postResult = client.PostAsync(uri, stringContent).Result;

            if (postResult.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("UserPanel");
            }

            return View("Index");
        }

        public IActionResult UserPanel()
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