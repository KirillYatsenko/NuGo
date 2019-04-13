using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventWebScrapper.Models;

namespace EventWebScrapper.Repositories
{
    public interface IEventsRepository
    {
        Task<bool> AddAsync(Event eventInfo);
        Task<bool> AddRangeAsync(IEnumerable<Event> eventInfos);
        IQueryable<Event> Get();
        IQueryable<Event> GetWithSchedules();
        Task<Event> GetAsync(long id);
        Task<bool> UpdateAsync(Event eventInfo);
        Task<bool> RemoveAsync(long id);
    }
}