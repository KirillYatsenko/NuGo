using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventWebScrapper.Models;

namespace EventWebScrapper.Repositories
{
    public class EventScrapDataRepository : IEventScrapDataRepository
    {
        private readonly EventWebScrapperDbContext _dbContext;

        public EventScrapDataRepository(EventWebScrapperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<EventScrapData> Get()
        {
            return _dbContext.EventScrapData.AsQueryable();
        }

        public async Task<EventScrapData> Get(long id)
        {
            return await _dbContext.EventScrapData.FindAsync(id);
        }

        public async Task<bool> Remove(long id)
        {
            var scrapData = await _dbContext.EventScrapData.FindAsync(id);
            var deleteResult = false;

            if (scrapData == null)
            {
                return deleteResult;
            }

            _dbContext.EventScrapData.Remove(scrapData);
            var itemsChangedCount = await _dbContext.SaveChangesAsync();

            if (itemsChangedCount > 0)
            {
                deleteResult = true;
            }

            return deleteResult;
        }

        public async Task<bool> Update(EventScrapData eventScrapData)
        {
            var updatedScrapData = _dbContext.Update(eventScrapData);
            var updateResult = await _dbContext.SaveChangesAsync();

            if (updateResult > 0)
            {
                return true;
            }

            return false;
        }

    }
}