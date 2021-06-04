using System.Security.Claims;
using Common.DTOs;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CQuerMVC.Helpers
{
    public static class GeneratePrincipal
    {
        public static ClaimsPrincipal GetPrincipal(UserDto userDto)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,userDto.Username),
                new Claim(ClaimTypes.Role, userDto.AccountType.ToString())
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            
            return new ClaimsPrincipal(identity);
        }
    }
}