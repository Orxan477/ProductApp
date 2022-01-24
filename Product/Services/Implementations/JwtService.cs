using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Product.Data.Entities;
using Product.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Product.Services.Implementations
{
    public class JwtService : IJwtService
    {
        private IConfiguration _configure;
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public JwtService(IConfiguration config)
        {
            _configure = config;
        }

        public string GenerateToken(AppUser user, IList<string> roles)
        {
            
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim("Fullname",user.FullName)
            };

            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                                                                    _configure.GetSection("Jwt:securityKey").Value));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: _configure.GetSection("Jwt:issuer").Value,
                audience: _configure.GetSection("Jwt:audience").Value,
                expires: DateTime.UtcNow.AddDays(1),
                claims: claims,
                signingCredentials: credentials
                );
            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
    }
}
