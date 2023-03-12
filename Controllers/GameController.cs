using Microsoft.AspNetCore.Mvc;
using App.Dto.Game;
using App.Services.GameService;

namespace App.Controllers
{
    [ApiController]
    [Route("game")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<ListGamesDto>> ListGames()
        {
            return await _gameService.ListGames();
        }

        [HttpGet("{gameId}")]
        public async Task<ActionResult<GameDto>> getGame([FromRoute] int gameId)
        {
            GameDto? game = await _gameService.GetGame(new GetGameDto { id = gameId });

            if (game == null)
            {
                return NotFound(new { error = "No se encontro el juego" });
            }
            else
            {
                return game;
            }
        }
    }
}
