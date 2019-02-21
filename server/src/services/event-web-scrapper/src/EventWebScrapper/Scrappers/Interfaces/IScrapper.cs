using System.Collections.Generic;
using System.Threading.Tasks;
using EventWebScrapper.Enums;
using EventWebScrapper.Models;

namespace EventWebScrapper.Scrappers
{
    public interface IScrapper
    {
        Task<IEnumerable<Event>> Scrap(EventCategories category);
    }
}