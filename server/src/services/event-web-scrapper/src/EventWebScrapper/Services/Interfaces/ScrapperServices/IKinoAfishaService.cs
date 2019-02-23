using System.Threading.Tasks;
using EventWebScrapper.Enums;
using EventWebScrapper.Models;

namespace EventWebScrapper.Services
{
    public interface IKinoAfishaService
    {
        Task<bool> ScrapAsync(EventCategories category);
    }
}