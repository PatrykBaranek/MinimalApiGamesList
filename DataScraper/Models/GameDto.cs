namespace DataScraper.Models;

public class GameDto
{
    public string GameTitle { get; set; } = null!;
    public string ImgUrl { get; set; } = null!;
    public string ReleaseDate { get; set; } = null!;
    public string MoreDetailUrl { get; set; } = null!;
    public string Platform { get; set; } = null!;
    public string MetaScore { get; set; } = null!;
}