using GameNow.Domain.Entities;
using BCrypt.Net;
using GameNow.Domain.Interfaces;
using GameNow.Server.Dtos;
using Microsoft.AspNetCore.Mvc;
using GameNow.Infrastructure.Repositories;
using GameNow.Server.Helpers;
using Microsoft.AspNetCore.Identity;

namespace GameNow.Server.Controllers
{
	[ApiController]
	[Route("/api/")]
	public class AuthController : Controller
	{
		private readonly IRepository<IdentityUser> _userRepository;
		private readonly UserManager<User> _userManager;
		private readonly JwtService _jwtService;


		public AuthController(IRepository<IdentityUser> userRepository, UserManager<User> userManager, JwtService jwtService)
		{
			_userRepository = userRepository;
			_userManager = userManager;
			_jwtService = jwtService;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterDto register)
		{
			//return Ok(register.Username);
			var user = new User
			{
				UserName = register.Username,
				Email = register.Email
			};

			return Created("success", await _userManager.CreateAsync(user, register.Password));
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginDto login)
		{
			var user = await _userManager.FindByEmailAsync(login.Email);

			if (user == null)
				return BadRequest(new { message = "Invalid Credentials" });

			if (await _userManager.CheckPasswordAsync(user, login.Password) == false)
				return BadRequest(new { message = "Invalid Credentials" });

			var jwt = _jwtService.Generate(Convert.ToInt32(user.Id));


			Response.Cookies.Append("jwt", jwt, new CookieOptions
			{
				HttpOnly = true
			});

			return Ok(new
			{
				message = "success"
			});
		}

		[HttpPost("user")]
		public IActionResult User()
		{
			try
			{
				string? jwt = Request.Cookies["jwt"];

				var token = _jwtService.Verify(jwt);

				int userId = int.Parse(token.Issuer);

				var user = _userRepository.GetById(userId);

				return Ok(user);
			}
			catch (Exception ex)
			{
				return Unauthorized();
			}
		}

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
