﻿using System.Text;
using System.Text.Encodings.Web;
using GameNow.Domain.Entities;
using BCrypt.Net;
using GameNow.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GameNow.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using GameNow.Server.Models;
using GameNow.Server.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Runtime.CompilerServices;

namespace GameNow.Server.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly EmailService _emailService;
        private readonly JwtService _jwtService;


        public AuthController(IRepository<IdentityUser> userRepository, UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
            JwtService jwtService, IEmailSender emailService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _emailService = (EmailService)emailService;
            _roleManager = roleManager;
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public IActionResult adminPing()
        {
            return Ok();
        }


        [Authorize]
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("hello");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel register)
        {
            var user = new User
            {
                UserName = register.Username,
                Email = register.Email,
            };

            var result = await _userManager.CreateAsync(user, register.Password);

            if (result.Succeeded)
            {
                var currentUser = await _userManager.FindByEmailAsync(register.Email);

                bool adminRoleExists = await _roleManager.RoleExistsAsync("Admin");
                bool userRoleExists = await _roleManager.RoleExistsAsync("User");

                if (!adminRoleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }
                if (!userRoleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                }


                await _userManager.AddToRoleAsync(user, "Admin");

                string baseUrl = $"{HttpContext.Request.Scheme}";

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Action("ConfirmEmail", "Auth", new { id = user.Id, code = code },
                    protocol: baseUrl);

                _emailService.SendEmailAsync(user.UserName, user.Email, callbackUrl!);
                return Created("success", result);
            }

            return BadRequest(result);

        }

        [HttpGet("confirm")]
        public async Task<ActionResult> ConfirmEmail(string code, string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

            if (user == null || user!.Id == null || code == null)
                return BadRequest();


            var result = await _userManager.ConfirmEmailAsync(user, code!);
            return Redirect("https://front-game-now.vercel.app/login");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel login)
        {
            User? user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, login.Password))
                return BadRequest(new { message = "Invalid Credentials" });


            var jwt = _jwtService.Generate(user);

            Response.Cookies.Append("X-Access-Token", jwt, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            Response.Cookies.Append("X-Username", user.UserName!, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });

            return Ok(jwt);
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("X-Access-Token");
            return Ok(new
            {
                message = "success"
            });
        }
    }
} 