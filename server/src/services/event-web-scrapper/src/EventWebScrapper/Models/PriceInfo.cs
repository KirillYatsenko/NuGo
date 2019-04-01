using System.ComponentModel.DataAnnotations;
using MySql.Data.EntityFrameworkCore.DataAnnotations;

namespace EventWebScrapper.Models
{
    public class PriceInfo
    {
        [Key]
        public int Id { get; set; }

        public EventSchedule Schedule { get; set; }

        public int EventDateId { get; set; }

        public decimal Min { get; set; }

        public decimal Max { get; set; }

        [MySqlCharset("utf8")]
        public string Currency { get; set; }
    }
}