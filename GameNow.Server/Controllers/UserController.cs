using GameNow.Domain.Entities;
using GameNow.Domain.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GameNow.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IRepository<User> _userRepository;

        public UserController(ILogger<UserController> logger, IRepository<User> userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            return Ok(_userRepository.GetAll());
        }

        [HttpGet("GetUser")]
        public IActionResult GetUserById(int Id)
        {
            return Ok(_userRepository.GetById(Id));
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