using System;
using System.Collections.Generic;
using System.Security.Claims;
using Common.DTOs;
using CommonServices.ClientService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp.Authenticators;

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
            if (ModelState.IsValid)
            {
                var response = _client.LoginResponse(loginDto);
                if (response.Result.IsSuccessful)
                {
                    return RedirectToAction("UserPanel");
                }
                return View(loginDto);
            }
            return View();
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