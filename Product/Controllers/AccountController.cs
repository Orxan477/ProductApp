using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Product.Data.Entities;
using Product.DTOs.AccountDto;
using Product.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IJwtService _jwtService;

        public AccountController(UserManager<AppUser> userManager,
                                 RoleManager<IdentityRole> roleManager,
                                 IJwtService jwtService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }

        //public async Task<ActionResult> Register()
        //{
        //    //await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        //    AppUser user = new AppUser
        //    {
        //        FullName = "Orkhan Ganbarov",
        //        Email = "orxan_qanbarov@mail.ru",
        //        UserName = "Orxan477"
        //    };
        //    await _userManager.CreateAsync(user, "Admin2002");
        //      await _userManager.AddToRoleAsync(user, "Admin");
        //    return Ok();
        //}
        [HttpPost]
        public async Task<ActionResult> Login(LoginDto login)
        {
            AppUser user = await _userManager.FindByNameAsync(login.Username);
            if (user is null) return NotFound();
            if (!await _userManager.CheckPasswordAsync(user,login.Password))
            {
                return Unauthorized();
            }
            var roles = await _userManager.GetRolesAsync(user);
            return Ok(_jwtService.GenerateToken(user,roles));
        }
    }
}
