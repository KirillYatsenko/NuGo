using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MySql.Data.EntityFrameworkCore.DataAnnotations;

namespace EventWebScrapper.Models
{
    public class Event
    {
        public Event() { }

        public Event(
            string title,
            string description,
            string detailsUrl,
            string imageUrl,
            int categoryId,
            decimal rating,
            List<EventDate> dates)
        {
            this.Title = title;
            this.Description = description;
            this.DetailsUrl = detailsUrl;
            this.ImagePath = imageUrl;
            this.CategoryId = categoryId;
            this.Rating = rating;
            this.Dates = dates;
        }

        [Key]
        public long Id { get; set; }

        public string DetailsUrl { get; set; }

        public bool Deleted { get; set; }

        [MySqlCharset("utf8")]
        public string Title { get; set; }

        [MySqlCharset("utf8")]
        public string Description { get; set; }

        public string ImagePath { get; set; }

        public int CategoryId { get; set; }
        public EventCategory Category { get; set; }

        public List<EventDate> Dates { get; set; }

        public decimal Rating { get; set; }
    }
}