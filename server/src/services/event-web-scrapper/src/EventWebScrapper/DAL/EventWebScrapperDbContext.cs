using System;
using System.Configuration;
using EventWebScrapper.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;

public class EventWebScrapperDbContext : DbContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<EventCategory> EventCategories { get; set; }
    public DbSet<EventDate> EventDates { get; set; }

    public EventWebScrapperDbContext(DbContextOptions options)
       : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Event>()
             .Property(r => r.Deleted)
             .HasConversion(new BoolToZeroOneConverter<Int16>());

        modelBuilder.Entity<EventDate>()
            .Property(r => r.Deleted)
            .HasConversion(new BoolToZeroOneConverter<Int16>());

        seedDatabase(modelBuilder);
    }

    private void seedDatabase(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventCategory>().HasData(
            new EventCategory
            {
                Id = 1,
                Name = "Cinema"
            }
        );
    }


}