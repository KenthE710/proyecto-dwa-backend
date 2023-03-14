namespace App.Dto.Game
{
    public class GameDto
    {
        public int id { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public string? cover { get; set; }
        public decimal? price { get; set; }
        public DateTime? releaseDate { get; set; }
        public bool isActive { get; set; }
        public int stars { get; set; }
        public List<string>? tags { get; set; }
        public List<string>? versions { get; set; }
        public List<string>? platforms { get; set; }
    }
}
