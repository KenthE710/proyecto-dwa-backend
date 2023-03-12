using App.Dto.Game;

namespace App.Services.GameService
{
    public interface IGameService
    {
        ///<summary>
        /// Retorna todos los juegos con la informacion necesaria para ser listada.
        ///</summary>
        ///<returns>
        /// Retorna un DTO con una lista de juegos.
        ///</returns>
        public Task<ListGamesDto> ListGames();

        ///<summary>
        /// Retorna los detalles de un juego.
        ///</summary>
        ///<returns>
        /// Retorna un DTO con los detalles del juegos.
        ///</returns>
        public Task<GameDto?> GetGame(GetGameDto getGameDto);
    }
}
