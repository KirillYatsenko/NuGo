using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventWebScrapper.Models;

namespace EventWebScrapper.Repositories
{
    public class EventsScheduleRepository : IEventsScheduleRepository
    {
        private readonly EventWebScrapperDbContext _dbContext;

        public EventsScheduleRepository(EventWebScrapperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(EventSchedule eventDate)
        {
            var result = await _dbContext.EventSchedules.AddAsync(eventDate);
            return await saveChangesAsync();
        }

        public async Task<bool> AddRangeAsync(IEnumerable<EventSchedule> eventDates)
        {
            await _dbContext.EventSchedules.AddRangeAsync(eventDates);
            return await saveChangesAsync();
        }

        public IQueryable<EventSchedule> Get()
        {
            return _dbContext.EventSchedules.AsQueryable();
        }

        public async Task<EventSchedule> GetAsync(long id)
        {
            return await _dbContext.EventSchedules.FindAsync(id);
        }

        public async Task<bool> RemoveAsync(long id)
        {
            var scrapData = await _dbContext.EventSchedules.FindAsync(id);

            if (scrapData == null)
            {
                return false;
            }

            scrapData.Deleted = true;
            return await UpdateAsync(scrapData);
        }

        public async Task<bool> RemoveRangeAsync(IEnumerable<EventSchedule> eventDates)
        {
            foreach (var date in eventDates)
            {
                date.Deleted = true;
            }

            return await UpdateRangeAsync(eventDates);
        }

        public async Task<bool> UpdateAsync(EventSchedule eventDate)
        {
            var updatedScrapData = _dbContext.Update(eventDate);
            return await saveChangesAsync();
        }

        public async Task<bool> UpdateRangeAsync(IEnumerable<EventSchedule> eventDates)
        {
            _dbContext.UpdateRange(eventDates);
            return await saveChangesAsync();
        }

        private async Task<bool> saveChangesAsync()
        {
            var rowsChangedCount = await _dbContext.SaveChangesAsync();

            if (rowsChangedCount >= 0)
            {
                return true;
            }

            return false;
        }
    }
}