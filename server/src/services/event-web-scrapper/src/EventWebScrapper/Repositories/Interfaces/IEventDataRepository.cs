using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventWebScrapper.Models;

namespace EventWebScrapper.Repositories
{
    public interface IEventDateRepository
    {
        Task<bool> AddAsync(EventDate eventDate);
        Task<bool> AddRangeAsync(IEnumerable<EventDate> eventDates);
        IQueryable<EventDate> Get();
        Task<EventDate> GetAsync(long id);
        Task<bool> UpdateAsync(EventDate eventDate);
        Task<bool> RemoveAsync(long id);
        Task<bool> RemoveRangeAsync(IEnumerable<EventDate> eventDates);
    }
}