using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventWebScrapper.Models;

namespace EventWebScrapper.Repositories
{
    public interface IEventsScheduleRepository
    {
        Task<bool> AddAsync(EventSchedule eventDate);
        Task<bool> AddRangeAsync(IEnumerable<EventSchedule> eventDates);
        IQueryable<EventSchedule> Get();
        Task<EventSchedule> GetAsync(long id);
        Task<bool> UpdateAsync(EventSchedule eventDate);
        Task<bool> RemoveAsync(long id);
        Task<bool> RemoveRangeAsync(IEnumerable<EventSchedule> eventDates);
    }
}