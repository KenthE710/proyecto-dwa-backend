namespace App.Dto.Game
{
    public class ListGameItem
    {
        public int id { get; set; }
        public string? title { get; set; }
        public string? cover { get; set; }
        public decimal? price { get; set; }
        public int stars { get; set; }
    }

    public class ListGamesDto
    {
        public List<ListGameItem>? games { get; set; }
    }
}
