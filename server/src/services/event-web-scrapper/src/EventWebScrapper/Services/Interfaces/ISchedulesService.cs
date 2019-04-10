using System.Linq;
using EventWebScrapper.Models;

namespace EventWebScrapper.Services
{
    public interface ISchedulesService
    {
        IQueryable<EventSchedule> Get();

        IQueryable<EventSchedule> Get(long eventId);
    }
}