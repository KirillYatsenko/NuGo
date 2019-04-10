using System.Linq;
using EventWebScrapper.Models;
using EventWebScrapper.Repositories;

namespace EventWebScrapper.Services
{
    public class SchedulesService : ISchedulesService
    {
        private readonly IEventsScheduleRepository _scheduleRepository;

        public SchedulesService(IEventsScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public IQueryable<EventSchedule> Get()
        {
            return _scheduleRepository.Get();
        }

        public IQueryable<EventSchedule> Get(long eventId)
        {
            var schedules = _scheduleRepository.Get();
            return schedules.Where(schedule=> @schedule.Event.Id == eventId);
        }
    }
}