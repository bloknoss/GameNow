using GameNow.Domain.Entities;
using GameNow.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GameNow.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private readonly IRepository<Game> _gameRepository;

        public GameController(ILogger<GameController> logger, IRepository<Game> gameRepository)
        {
            _logger = logger;
            _gameRepository = gameRepository;
        }

        [HttpGet("GetGames")]
        public IActionResult GetGames()
        {
            return Ok(_gameRepository.GetAll());
        }

        [HttpGet("GetGame")]
        public IActionResult GetGameById(int Id)
        {
            return Ok(_gameRepository.GetById(Id));


        }
        [HttpPost("InsertGame")]
        public IActionResult InsertGame(Game game)
        {
            return Ok(_gameRepository.Add(game));
        }

        [HttpDelete("DeleteGame")]
        public IActionResult Delete(int Id)
        {
            _gameRepository.Delete(Id);
            return Ok();
        }

        [HttpPut("UpdateGame")]
        public IActionResult Update(Game game)
        {
            _gameRepository.Update(game);
            return Ok();
        }
    }
}