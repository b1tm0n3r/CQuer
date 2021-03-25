using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Common.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace CommonServices.TokenService
{
    public class TokenService : ITokenService
    {
        public string CreateToken(LoginDto loginDto)
        {
            var jwtSettings = new JwtSettings();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginDto.Username),
            };
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(jwtSettings.LifetimeInSeconds),
                SigningCredentials = credentials,
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }
    }
}