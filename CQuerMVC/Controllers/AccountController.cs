﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Common.DataModels.IdentityManagement;
using Common.DTOs;
using CommonServices.ClientService.AccountClient;
using CQuerMVC.Helpers;
using CQuerMVC.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CQuerMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountClientService _accountClientService;
        private readonly IValidator _registerValidator;
        public AccountController(IAccountClientService accountClientService, IValidator<RegisterDto> registerValidator)
        {
            _accountClientService = accountClientService;
            _registerValidator = registerValidator;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return returnUrl is null ? View() : View(new LoginViewModel() { RedirectUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View();

            var response = _accountClientService.LoginResponse(loginViewModel.LoginDto);
            if (response.Result.IsSuccessful)
            {
                var signInUser = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDto>(response.Result.Content);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, GeneratePrincipal.GetPrincipal(signInUser));
                return loginViewModel.RedirectUrl is null ? RedirectToAction("Index", "Home") : Redirect(loginViewModel.RedirectUrl);
            }
            ModelState.AddModelError(nameof(LoginDto.Password), "Invalid username or password!");
            return View(loginViewModel);
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
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterStandardUserViewModel registerDto)
        {
            IValidationContext validationContext = new ValidationContext<RegisterDto>(registerDto);
            if (!ModelState.IsValid || !_registerValidator.Validate(validationContext).IsValid)
                return View();

            var registerResponse = _accountClientService.RegisterResponse(registerDto);
            if (registerResponse.Result.IsSuccessful)
            {
                var location = _accountClientService.GetUserLocation(await registerResponse);
                var id = new string(location.Where(Char.IsDigit).ToArray());
                var signInUser = _accountClientService.GetUserDtoById(Int32.Parse(id));
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, GeneratePrincipal.GetPrincipal(signInUser.Result));

                return RedirectToAction("Index", "Account");
            }
            return View();
        }
        
        [EnumAuthorizeRole(AccountType.Administrator)]
        public IActionResult RegisterAdminPanel()
        {
            return View();
        }

        [HttpPost]
        [EnumAuthorizeRole(AccountType.Administrator)]
        public async Task<IActionResult> RegisterAdminPanel(RegisterAdminViewModel registerDto)
        {
            IValidationContext validationContext = new ValidationContext<RegisterDto>(registerDto);
            if (!ModelState.IsValid || !_registerValidator.Validate(validationContext).IsValid)
                return View();
            
            var registerResponse = await _accountClientService.RegisterResponse(registerDto);
            if (registerResponse.IsSuccessful)
            {
                ViewBag.Result = "Account created successfully!";
                return View();
            }
            ModelState.AddModelError(nameof(RegisterAdminViewModel.Username), registerResponse.Content.Trim('"'));

            return View();
        }
    }
}