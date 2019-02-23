using System.Threading.Tasks;

namespace EventWebScrapper.Services
{
    public interface IFileDownloaderService
    {
        Task<string> DownloadFile(string fileUrl, string filePrefix);
    }
}