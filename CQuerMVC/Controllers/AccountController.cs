using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Common.DataModels.IdentityManagement;
using Common.DTOs;
using CommonServices.ClientService;
using CQuerMVC.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

        [EnumAuthorizeRole(AccountType.StandardUser)]
        public IActionResult UserPanel()
        {
            return View();
        }
        
        [EnumAuthorizeRole(AccountType.Administrator)]
        public IActionResult AdminPanel()
        {
            return View();
        }
        
    }
}