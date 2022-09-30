using Microsoft.EntityFrameworkCore;

namespace DataScraper.Entities;

public class GameDbContext : DbContext
{
    public GameDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Game> Game { get; set; } = null!;
    public DbSet<Platform> Platform { get; set; } = null!;
    public DbSet<GamePlatform> GamePlatform { get; set; } = null!;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>()
            .Property(x => x.GameTitle)
            .IsRequired();

        modelBuilder.Entity<Game>()
            .HasMany(x => x.GamePlatforms)
            .WithOne(x => x.Game);


        modelBuilder.Entity<Platform>()
            .HasData(Platforms);
    }
}