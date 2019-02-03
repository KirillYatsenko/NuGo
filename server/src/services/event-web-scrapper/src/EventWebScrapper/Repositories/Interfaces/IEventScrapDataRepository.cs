using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventWebScrapper.Models;

namespace EventWebScrapper.Repositories
{
    public interface IEventScrapDataRepository
    {
        IQueryable<EventScrapData> Get();
        Task<EventScrapData> Get(long id);
        Task<bool> Update(EventScrapData eventScrapData);
        Task<bool> Remove(long id);
    }
}