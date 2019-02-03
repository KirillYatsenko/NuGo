using System.Configuration;
using EventWebScrapper.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class EventWebScrapperDbContext : DbContext
{
    private readonly string _sqlConnection;

    public EventWebScrapperDbContext(DbContextOptions options)
       : base(options) { }

    // public EventWebScrapperDbContext(IConfiguration configuration)
    // {
    //     _sqlConnection = configuration["ConnectionString "];

    //     if (string.IsNullOrWhiteSpace(_sqlConnection))
    //     {
    //         throw new ConfigurationErrorsException("ConnectionString entry can not be found, please check appsettings");
    //     }
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EventScrapData>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired();
        });
    }

    public DbSet<EventScrapData> EventScrapData { get; set; }
}