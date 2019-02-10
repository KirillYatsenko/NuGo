using System.Threading.Tasks;

namespace EventWebScrapper.Services
{
    public interface IKinoAfishaScrapperService
    {
        Task<bool> ScrapAsync();
    }
}