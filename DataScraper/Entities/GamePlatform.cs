namespace DataScraper.Entities;

public class GamePlatform
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public int PlatformId { get; set; }
    public Platform Platform { get; set; } = null!;

    public Guid GameId { get; set; } 
    public Game Game { get; set; } = null!;
}