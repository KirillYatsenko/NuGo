using System.Threading.Tasks;

namespace EventWebScrapper.Scrappers
{
    public interface IEventImageScrapper
    {
        Task<string> ScrapImage(string imageUrl, string eventTitle);
    }
}