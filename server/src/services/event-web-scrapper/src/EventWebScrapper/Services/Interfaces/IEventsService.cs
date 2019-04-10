using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventWebScrapper.Models;

namespace EventWebScrapper.Services
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetRelevantAsync();
        IQueryable<Event> GetHistoryAsync();
        Task<Event> GetAsync(long id);
    }
}