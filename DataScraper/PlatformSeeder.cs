using DataScraper.Entities;

namespace DataScraper;

public class PlatformSeeder
{

    private static readonly List<Platform> Platforms = new()
    {
        new Platform
        {
            Id = 1,
            PlatformName = "PlayStation 5"
        },
        new Platform
        {
            Id = 2,
            PlatformName = "PlayStation 4"
        },
        new Platform
        {
            Id = 3,
            PlatformName = "PC"
        },
        new Platform
        {
            Id = 4,
            PlatformName = "Xbox Series X"
        },
        new Platform
        {
            Id = 5,
            PlatformName = "Xbox One"
        },
        new Platform
        {
            Id = 6,
            PlatformName = "Switch"
        }
    };

    public static async Task SeedPlatform(GameDbContext dbContext)
    {
        if (!dbContext.Platform.Any())
        {
            await dbContext.Platform.AddRangeAsync(Platforms);
            await dbContext.SaveChangesAsync();
        }
    }

}