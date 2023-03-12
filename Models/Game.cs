namespace App.Models
{
    public class Game
    {
        public int id { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public string? cover { get; set; }
        public decimal? price { get; set; }
        public List<Tag>? tags { get; set; }
        public DateTime releaseDate { get; set; }
        public int stock { get; set; }
        public bool isActive { get; set; }
        public int stars { get; set; }
    }
}
