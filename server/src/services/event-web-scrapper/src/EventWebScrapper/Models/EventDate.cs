using System;
using System.ComponentModel.DataAnnotations;
using MySql.Data.EntityFrameworkCore.DataAnnotations;

namespace EventWebScrapper.Models
{
    public class EventDate
    {
        [Key]
        public int Id { get; set; }

        public Event Event { get; set; }

        public PriceInfo Price { get; set; }

        public DateTime Date { get; set; }

        public bool Deleted { get; set; }

        [MySqlCharset("utf8")]
        public string HostName { get; set; }

        [MySqlCharset("utf8")]
        public string Address { get; set; }
    }
}