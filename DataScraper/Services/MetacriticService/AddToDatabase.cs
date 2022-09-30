using DataScraper.Entities;
using DataScraper.Models;
using Microsoft.EntityFrameworkCore;

namespace DataScraper.Services.MetacriticService;

public class AddToDatabase
{
    public static async Task AddGames(List<GameDto> games, GameDbContext dbContext)
    {
        foreach (var gameDto in games)
        {
            var newGame = FormatGame(gameDto);

            var platform = await dbContext.Platform.FirstOrDefaultAsync(x => x.PlatformName == gameDto.Platform);

            var platformGame = new GamePlatform
            {
                GameId = newGame.Id,
                Platform = platform
            };

            if (await dbContext.Game.AnyAsync(x => x.GameTitle == gameDto.GameTitle))
            {
                var gameId = dbContext.Game.FirstOrDefault(x => x.GameTitle == newGame.GameTitle).Id;

                platformGame.GameId = gameId;

                await dbContext.AddAsync(platformGame);

                continue;
            }

            await dbContext.AddRangeAsync(newGame, platformGame);
            await dbContext.SaveChangesAsync();
        }
    }

    private static Game FormatGame(GameDto dto)
    {
        var newGame = new Game
        {
            GameTitle = dto.GameTitle,
            ImgUrl = dto.ImgUrl,
            MetaScore = dto.MetaScore == string.Empty ? 0 : int.Parse(dto.MetaScore),
            ReleaseDate = FormatReleaseDate(dto.ReleaseDate),
            MoreDetailUrl = "https://metacritic.com" + dto.MoreDetailUrl,
        };

        return newGame;
    }

    private static DateTime FormatReleaseDate(string dateToFormat)
    {
        var dateAsArrayString = dateToFormat.Split(' ');

        var month = dateAsArrayString[0];
        var day = dateAsArrayString[1].Replace(",", "");
        var year = dateAsArrayString[2];

        return DateTime.Parse(string.Join("/", month, day, year));
    }

}