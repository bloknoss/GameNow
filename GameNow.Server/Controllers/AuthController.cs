using System.Text;
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

namespace GameNow.Server.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly EmailService _emailService;
        private readonly JwtService _jwtService;


        public AuthController(IRepository<IdentityUser> userRepository, UserManager<User> userManager,
            JwtService jwtService, IEmailSender emailService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _emailService = (EmailService?)emailService;
        }

        [HttpGet("test")]
        public IActionResult test()
        {
            return Ok("eee");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel register)
        {
            var user = new User
            {
                UserName = register.Username,
                Email = register.Email
            };

            var result = await _userManager.CreateAsync(user, register.Password);

            if (result.Succeeded)
            {
                string baseUrl = $"{HttpContext.Request.Scheme}";

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Action("ConfirmEmail", "Auth", new { id = user.Id, code = code },
                    protocol: baseUrl);
                await _emailService.SendEmailAsync(user.UserName, user.Email, callbackUrl!);
            }

            return Created("success", result);
        }

        [HttpGet("confirm")]
        public async Task<ActionResult> ConfirmEmail(string code, string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

            if (user.Id == null || code == null)
                return BadRequest();


            var result = await _userManager.ConfirmEmailAsync(user, code!);
            return Ok(result);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, login.Password))
                return BadRequest(new { message = "Invalid Credentials" });

            var jwt = _jwtService.Generate(user);

            return Ok(jwt);
        }

        [Authorize]
        [HttpPost("user")]
        public IActionResult User()
        {
            return Ok("hello");
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new
            {
                message = "success"
            });
        }
    }
}