using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventWebScrapper.Models;

namespace EventWebScrapper.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventWebScrapperDbContext _dbContext;

        public EventRepository(EventWebScrapperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(Event eventScrapData)
        {
            var result = await _dbContext.Events.AddAsync(eventScrapData);
            return await saveChangesAsync();
        }

        public async Task<bool> AddRangeAsync(IEnumerable<Event> eventScrapData)
        {
            await _dbContext.Events.AddRangeAsync(eventScrapData);
            return await saveChangesAsync();
        }

        public IQueryable<Event> Get()
        {
            return _dbContext.Events.AsQueryable();
        }

        public async Task<Event> GetAsync(long id)
        {
            return await _dbContext.Events.FindAsync(id);
        }

        public async Task<bool> RemoveAsync(long id)
        {
            var scrapData = await _dbContext.Events.FindAsync(id);
            
            if (scrapData == null)
            {
                return false;
            }
                
            scrapData.Deleted = true;
            return await UpdateAsync(scrapData);
        }

        public async Task<bool> UpdateAsync(Event eventScrapData)
        {
            var updatedScrapData = _dbContext.Update(eventScrapData);
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