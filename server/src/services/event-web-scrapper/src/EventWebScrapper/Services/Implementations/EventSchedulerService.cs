using System;
using System.Linq;
using System.Threading.Tasks;
using EventWebScrapper.Models;
using Microsoft.EntityFrameworkCore;

namespace EventWebScrapper.Services
{
    public class EventScheduler : IEventScheduler
    {
        private readonly ISchedulesService _schedulesService;

        public EventScheduler(ISchedulesService schedulesService)
        {
            _schedulesService = schedulesService;
        }

        public async void SetClosestSchedule(Event @event)
        {
            var eventSchedules = _schedulesService.Get(@event.Id);
            var closestSchedule = await eventSchedules.Where(schedule => schedule.Date > DateTime.Now)
                                                      .OrderBy(schedule => schedule.Date)
                                                      .FirstOrDefaultAsync();

            @event.ClosestSchedule = closestSchedule;
        }
    }
}