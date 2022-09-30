using DataScraper.Entities;
using DataScraper.Models;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;

namespace DataScraper.Services.MetacriticService;

public class ScrapeGames : IScrapeGames
{
    private readonly Dictionary<Platforms, string> _metacriticUrls = new()
    {
        { Platforms.PC , "https://www.metacritic.com/browse/games/score/metascore/all/pc/filtered?sort=desc&view=condensed" },
        { Platforms.Ps5, "https://www.metacritic.com/browse/games/score/metascore/all/ps5/filtered?view=condensed&sort=desc" },
        { Platforms.Ps4, "https://www.metacritic.com/browse/games/score/metascore/all/ps4/filtered?view=condensed&sort=desc" },
        { Platforms.Switch, "https://www.metacritic.com/browse/games/score/metascore/all/switch/filtered?view=condensed&sort=desc" },
        { Platforms.XboxSeriesX, "https://www.metacritic.com/browse/games/score/metascore/all/xbox-series-x/filtered?sort=desc&view=condensed" },
        { Platforms.XboxOne, "https://www.metacritic.com/browse/games/score/metascore/all/xboxone/filtered?sort=desc&view=condensed" }
    };

    private readonly GameDbContext _dbContext;
    public ScrapeGames(GameDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<GameDto>> ScrapeDataFromMetacritic(Platforms platform)
    {
        HtmlWeb web = new();
        var doc = await web.LoadFromWebAsync(_metacriticUrls.GetValueOrDefault(platform));

        var games = new List<GameDto>();

        var lastPage = int.Parse(doc.QuerySelector("li.page.last_page a.page_num").InnerText);

        for (int i = 0; i <= lastPage - 1; i++)
        {
            doc = await web.LoadFromWebAsync(_metacriticUrls.GetValueOrDefault(platform) + $"&page={i}");
            var gamesOnPage = doc.QuerySelectorAll("tr.expand_collapse").ToList();

            foreach (var game in gamesOnPage)
            {
                var gameTitle = game.QuerySelector("a.title h3").InnerText;
                var imgUrl = game.QuerySelector("img").Attributes["src"].Value;
                var metaScore = game.QuerySelector("div.metascore_w") != null ?
                    game.QuerySelector("div.metascore_w").InnerText : string.Empty;
                var platformString = game.QuerySelector(".platform span.data").InnerText.Replace("\n", "").Trim();
                var releaseDate = game.QuerySelector(".platform+span").InnerText;
                var moreDetails = game.QuerySelector(".details a.title").Attributes["href"].Value;

                games.Add(new GameDto
                {
                    GameTitle = gameTitle,
                    ImgUrl = imgUrl,
                    MetaScore = metaScore,
                    Platform = platformString,
                    ReleaseDate = releaseDate,
                    MoreDetailUrl = moreDetails
                });
            }
        }

        await AddToDatabase.AddGames(games, _dbContext);

        return games;
    }
}