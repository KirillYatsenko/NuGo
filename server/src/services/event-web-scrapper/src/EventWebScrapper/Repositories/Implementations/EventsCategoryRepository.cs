using System.Linq;
using EventWebScrapper.Models;

namespace EventWebScrapper.Repositories
{
    public class EventsCategoryRepository : IEventsCategoryRepository
    {
        private readonly EventWebScrapperDbContext _dbContext;

        public EventsCategoryRepository(EventWebScrapperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<EventCategory> Get()
        {
            return _dbContext.EventCategories;
        }
    }
}