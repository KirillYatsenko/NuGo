using EventWebScrapper.Models;

namespace EventWebScrapper.Services
{
    public interface IEventScheduler
    {
        void SetClosestSchedule(Event @event);
    }
}