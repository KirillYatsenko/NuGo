﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EventWebScrapper.Migrations
{
    [DbContext(typeof(EventWebScrapperDbContext))]
    [Migration("20190224135039_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity("EventWebScrapper.Models.Event", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<short>("Deleted");

                    b.Property<string>("Description")
                        .HasAnnotation("MySQL:Charset", "utf8");

                    b.Property<string>("DetailsUrl");

                    b.Property<string>("ImagePath");

                    b.Property<decimal>("Rating");

                    b.Property<string>("Title")
                        .HasAnnotation("MySQL:Charset", "utf8");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("EventWebScrapper.Models.EventCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasAnnotation("MySQL:Charset", "utf8");

                    b.HasKey("Id");

                    b.ToTable("EventCategories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Cinema"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Concerts"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Theater"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Party"
                        });
                });

            modelBuilder.Entity("EventWebScrapper.Models.EventDate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasAnnotation("MySQL:Charset", "utf8");

                    b.Property<DateTime>("Date");

                    b.Property<short>("Deleted");

                    b.Property<long?>("EventId");

                    b.Property<string>("HostName")
                        .HasAnnotation("MySQL:Charset", "utf8");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("EventDates");
                });

            modelBuilder.Entity("EventWebScrapper.Models.PriceInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Currency")
                        .HasAnnotation("MySQL:Charset", "utf8");

                    b.Property<int>("EventDateId");

                    b.Property<decimal>("Max");

                    b.Property<decimal>("Min");

                    b.HasKey("Id");

                    b.HasIndex("EventDateId")
                        .IsUnique();

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("EventWebScrapper.Models.Event", b =>
                {
                    b.HasOne("EventWebScrapper.Models.EventCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EventWebScrapper.Models.EventDate", b =>
                {
                    b.HasOne("EventWebScrapper.Models.Event", "Event")
                        .WithMany("Dates")
                        .HasForeignKey("EventId");
                });

            modelBuilder.Entity("EventWebScrapper.Models.PriceInfo", b =>
                {
                    b.HasOne("EventWebScrapper.Models.EventDate", "EventDate")
                        .WithOne("Price")
                        .HasForeignKey("EventWebScrapper.Models.PriceInfo", "EventDateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}