using System.Threading.Tasks;
using EventWebScrapper.Enums;

namespace EventWebScrapper.Services
{
    public interface IKoncertUAService
    {
        Task<bool> ScrapAsync(EventCategories category);
    }
}