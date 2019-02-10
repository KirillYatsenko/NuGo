using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MySql.Data.EntityFrameworkCore.DataAnnotations;

namespace EventWebScrapper.Models
{
    public class Event
    {
        [Key]
        public long Id { get; set; }

        public string DetailsUrl { get; set; }

        public bool Deleted { get; set; }

        [MySqlCharset("utf8")]
        public string Title { get; set; }

        [MySqlCharset("utf8")]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public EventCategory Category { get; set; }

        public List<EventDate> Dates { get; set; }

        public decimal Rating { get; set; }
    }
}