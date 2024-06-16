using GameNow.Domain.Entities;
using GameNow.Domain.Interfaces;
using GameNow.Infrastructure.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameNow.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserRepository _userRepository;

        public UserController(ILogger<UserController> logger, IRepository<IdentityUser> userRepository)
        {
            _logger = logger;
            _userRepository = (UserRepository)userRepository;
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            return Ok(_userRepository.GetAll());
        }

        [HttpGet("GetUser")]
        public IActionResult GetUser(string Email)
        {
            return Ok(_userRepository.GetByEmail(Email));
        }

        [HttpDelete("DeleteUser")]
        public IActionResult Delete(int Id)
        {
            _userRepository.Delete(Id);
            return Ok();
        }

        [HttpPut("UpdateUser")]
        public IActionResult Update(User user)
        {
            _userRepository.Update(user);
            return Ok();
        }
    }
}