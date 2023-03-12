using System.Data;
using System.Xml.Linq;
using App.DBMagnament;
using App.Dto.Game;

namespace App.Services.GameService
{
    public class GameService : IGameService
    {
        public async Task<ListGamesDto> ListGames()
        {
            DataSet dsResultado = await DBXmlMethods.EjecutaBase("GetGames", "LIST_GAMES");

            List<ListGameItem> ListData = new List<ListGameItem>();

            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    foreach (DataRow row in dsResultado.Tables[0].Rows)
                    {
                        ListData.Add(
                            new ListGameItem
                            {
                                id = Convert.ToInt32(row["id"]),
                                title = row["title"].ToString(),
                                cover = row["cover"].ToString(),
                                price = Convert.ToDecimal(row["price"]),
                                stars = Convert.ToInt32(row["stars"]),
                            }
                        );
                    }
                }
                catch (Exception)
                {
                    Console.Write("Error");
                }
            }

            return new ListGamesDto { games = ListData };
        }

        public async Task<GameDto?> GetGame(GetGameDto getGameDto)
        {
            XDocument? xmlParam = DBXmlMethods.GetXml(getGameDto);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(
                "GetGames",
                "GET_GAME",
                xmlParam?.ToString()
            );

            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    var rows = dsResultado.Tables[0].Rows;
                    var firstRow = rows[0];

                    List<string> tags = new List<string>();
                    List<string> versions = new List<string>();
                    List<string> platforms = new List<string>();

                    foreach (DataRow row in rows)
                    {
                        tags.Add(row["tag"].ToString()!);
                        versions.Add(row["version"].ToString()!);
                        platforms.Add(row["platform"].ToString()!);
                    }

                    return new GameDto
                    {
                        id = Convert.ToInt32(firstRow["id"]),
                        title = firstRow["title"].ToString(),
                        description = firstRow["description"].ToString(),
                        cover = firstRow["cover"].ToString(),
                        price = Convert.ToInt32(firstRow["price"]),
                        releaseDate = DateTime.Parse(firstRow["release_date"].ToString()!),
                        isActive = Convert.ToBoolean(firstRow["is_active"]),
                        stars = Convert.ToInt32(firstRow["stars"]),
                        tags = tags.Distinct().ToList(),
                        versions = versions.Distinct().ToList(),
                        platforms = platforms.Distinct().ToList()
                    };
                }
                catch (Exception)
                {
                    Console.Write("Error");
                }
            }

            return null;
        }
    }
}
