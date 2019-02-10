using System.ComponentModel.DataAnnotations;
using MySql.Data.EntityFrameworkCore.DataAnnotations;

namespace EventWebScrapper.Models
{
    public class EventCategory
    {
        [Key]
        public int Id { get; set; }

        [MySqlCharset("utf8")]
        public string Name { get; set; }
    }
}