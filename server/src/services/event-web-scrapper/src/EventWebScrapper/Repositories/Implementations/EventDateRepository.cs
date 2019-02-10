using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventWebScrapper.Models;

namespace EventWebScrapper.Repositories
{
    public class EventDateRepository : IEventDateRepository
    {
        private readonly EventWebScrapperDbContext _dbContext;

        public EventDateRepository(EventWebScrapperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(EventDate eventDate)
        {
            var result = await _dbContext.EventDates.AddAsync(eventDate);
            return await saveChangesAsync();
        }

        public async Task<bool> AddRangeAsync(IEnumerable<EventDate> eventDates)
        {
            await _dbContext.EventDates.AddRangeAsync(eventDates);
            return await saveChangesAsync();
        }

        public IQueryable<EventDate> Get()
        {
            return _dbContext.EventDates.AsQueryable();
        }

        public async Task<EventDate> GetAsync(long id)
        {
            return await _dbContext.EventDates.FindAsync(id);
        }

        public async Task<bool> RemoveAsync(long id)
        {
            var scrapData = await _dbContext.EventDates.FindAsync(id);

            if (scrapData == null)
            {
                return false;
            }

            scrapData.Deleted = true;
            return await UpdateAsync(scrapData);
        }

        public async Task<bool> RemoveRangeAsync(IEnumerable<EventDate> eventDates)
        {
            foreach (var date in eventDates)
            {
                date.Deleted = true;
            }

            return await UpdateRangeAsync(eventDates);
        }

        public async Task<bool> UpdateAsync(EventDate eventDate)
        {
            var updatedScrapData = _dbContext.Update(eventDate);
            return await saveChangesAsync();
        }

        public async Task<bool> UpdateRangeAsync(IEnumerable<EventDate> eventDates)
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