namespace DataScraper.Entities
{
    public class Game
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string GameTitle { get; set; } = null!;
        public string ImgUrl { get; set; } = null!;
        public string MoreDetailUrl { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public int MetaScore { get; set; }

        public List<GamePlatform> GamePlatforms { get; set; } = null!;
    }
}
