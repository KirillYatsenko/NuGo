
using System.Linq;
using System.Threading.Tasks;
using EventWebScrapper.Models;

namespace EventWebScrapper.Repositories
{
    public interface IEventsCategoryRepository
    {
        IQueryable<EventCategory> Get();
    }

}