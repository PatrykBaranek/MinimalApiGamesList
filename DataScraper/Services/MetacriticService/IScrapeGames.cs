using DataScraper.Models;

namespace DataScraper.Services.MetacriticService;

public interface IScrapeGames
{
    Task<IEnumerable<GameDto>> ScrapeDataFromMetacritic(Platforms platform);
}