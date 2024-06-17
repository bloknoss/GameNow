using GameNow.Database;
using GameNow.Domain.Entities;
using GameNow.Domain.Interfaces;
using GameNow.Infrastructure.Repositories;
using Mailjet.Client.Resources;
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
        public IActionResult GetUser(string Id)
        {
            return Ok(_userRepository.GetById(Id));
        }


        [HttpPost("InsertUser")]
        public IActionResult InsertUser(Domain.Entities.User user)
        {
            _userRepository.Add(user);
            return Ok();
        }

        [HttpDelete("DeleteUser")]
        public IActionResult Delete(string Id)
        {
            _userRepository.Delete(Id);
            return Ok();
        }

        [HttpPut("UpdateUser")]
        public IActionResult Update(Domain.Entities.User user)
        {
            _userRepository.Update(user);
            return Ok();
        }
    }
}