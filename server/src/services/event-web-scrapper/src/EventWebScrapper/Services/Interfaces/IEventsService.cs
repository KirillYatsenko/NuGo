using System.Linq;
using System.Threading.Tasks;
using EventWebScrapper.Models;

namespace EventWebScrapper.Services
{
    public interface IEventService
    {
        IQueryable<Event> Get();
        Task<Event> GetAsync(long id);
    }
}