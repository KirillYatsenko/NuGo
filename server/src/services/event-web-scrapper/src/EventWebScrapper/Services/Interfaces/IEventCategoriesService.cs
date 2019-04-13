using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventWebScrapper.Models;

namespace EventWebScrapper.Services
{
    public interface IEventsCategoryService
    {
        IQueryable<EventCategory> Get();
    }
}