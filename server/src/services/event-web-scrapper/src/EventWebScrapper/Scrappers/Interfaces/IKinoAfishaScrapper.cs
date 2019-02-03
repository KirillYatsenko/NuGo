using System.Collections.Generic;
using System.Threading.Tasks;
using EventWebScrapper.Models;

namespace EventWebScrapper.Scrappers
{
    public interface IKinoAfishaScrapper
    {
        Task<IEnumerable<EventScrapData>> Scrap();
    }
}