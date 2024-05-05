﻿using GameNow.Domain.Entities;
using BCrypt.Net;
using GameNow.Domain.Interfaces;
using GameNow.Server.Dtos;
using Microsoft.AspNetCore.Mvc;
using GameNow.Infrastructure.Repositories;
using GameNow.Server.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

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