using System.Threading.Tasks;

namespace EventWebScrapper.Services
{
    public interface IFileBrowserService
    {
        bool EnsureExists(string path);
    }
}