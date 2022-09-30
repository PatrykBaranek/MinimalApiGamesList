using DataScraper;
using DataScraper.Entities;
using DataScraper.Requests;
using DataScraper.Services.MetacriticService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IScrapeGames, ScrapeGames>();
//builder.Services.AddDbContext<GameDbContext>(options =>
//{
//    options.UseInMemoryDatabase("GameDb");
//}, optionsLifetime: ServiceLifetime.Scoped);

//builder.Services.AddDbContext<GameDbContext>(opt =>
//{
//    opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnectionString"));
//}, optionsLifetime: ServiceLifetime.Scoped);

builder.Services.AddDbContext<GameDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnectionString"));
}, optionsLifetime: ServiceLifetime.Scoped);

var app = builder.Build();

//var scope = app.Services.CreateScope();
//var dbContext = scope.ServiceProvider.GetRequiredService<GameDbContext>();
//await PlatformSeeder.SeedPlatform(dbContext);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MetacriticRequestsHandler();


app.Run();