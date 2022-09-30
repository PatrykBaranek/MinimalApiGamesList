using DataScraper.Entities;
using DataScraper.Models;
using DataScraper.Services.MetacriticService;
using Microsoft.EntityFrameworkCore;

namespace DataScraper.Requests;

public static class MetacriticRequests
{
    public static WebApplication MetacriticRequestsHandler(this WebApplication app)
    {
        app.MapGet("/ps5", MetacriticRequests.GetPs5Games)
            .Produces<List<GameDto>>();

        app.MapGet("/ps4", MetacriticRequests.GetPs4Games)
            .Produces<List<GameDto>>();

        app.MapGet("/pc", MetacriticRequests.GetPcGames)
            .Produces<List<GameDto>>();

        app.MapGet("/xboxseriesx", MetacriticRequests.GetXboxSeriesGames)
            .Produces<List<GameDto>>();

        app.MapGet("/xboxone", MetacriticRequests.GetXboxOneGames)
            .Produces<List<GameDto>>();

        app.MapGet("/switch", MetacriticRequests.GetSwitchGames)
            .Produces<List<GameDto>>();

        app.MapGet("/games", async (GameDbContext gameDbContext) =>
        {
            var games = await gameDbContext.Game.ToListAsync();

            return games;
        });

        app.MapGet("/gamePlatforms", async (GameDbContext gameDbContext) =>
        {
            var gamePlatforms = await gameDbContext.GamePlatform.ToListAsync();

            return gamePlatforms;
        });

        app.MapGet("/gamePlatform/{gameId:guid}",async (GameDbContext gameDbContext, Guid gameId) =>
        {
            var gamePlatforms = await gameDbContext.GamePlatform.Where(x => x.GameId == gameId).ToListAsync();

            return gamePlatforms;
        });


        return app;
    }


    public static async Task<IResult> GetPs5Games(IScrapeGames service)
    {
        return Results.Ok(await service.ScrapeDataFromMetacritic(Platforms.Ps5));
    }

    public static async Task<IResult> GetPs4Games(IScrapeGames service)
    {
        return Results.Ok(await service.ScrapeDataFromMetacritic(Platforms.Ps4));
    }

    public static async Task<IResult> GetPcGames(IScrapeGames service)
    {
        return Results.Ok(await service.ScrapeDataFromMetacritic(Platforms.PC));
    }

    public static async Task<IResult> GetXboxSeriesGames(IScrapeGames service)
    {
        return Results.Ok(await service.ScrapeDataFromMetacritic(Platforms.XboxSeriesX));
    }

    public static async Task<IResult> GetXboxOneGames(IScrapeGames service)
    {
        return Results.Ok(await service.ScrapeDataFromMetacritic(Platforms.XboxOne));
    }

    public static async Task<IResult> GetSwitchGames(IScrapeGames service)
    {
        return Results.Ok(await service.ScrapeDataFromMetacritic(Platforms.Switch));
    }


}