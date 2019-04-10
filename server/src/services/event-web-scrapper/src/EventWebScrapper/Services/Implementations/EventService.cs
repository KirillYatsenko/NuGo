using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventWebScrapper.Models;
using EventWebScrapper.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventWebScrapper.Services
{
    public class EventService : IEventService
    {
        private readonly IEventsRepository _eventRepository;
        private readonly IEventScheduler _scheduler;

        public EventService(IEventsRepository eventRepository,
                            IEventScheduler scheduler)
        {
            _eventRepository = eventRepository;
            _scheduler = scheduler;
        }

        public async Task<IEnumerable<Event>> GetRelevantAsync()
        {
            var relevantEvents = await _eventRepository.Get()
                                    .Where(@event => @event.Schedules
                                        .Any(schedule => schedule.Date > DateTime.Now)
                                        && @event.Deleted == false
                                    ).ToListAsync();

            foreach (var @event in relevantEvents)
            {
                _scheduler.SetClosestSchedule(@event);
            }

            return relevantEvents;
        }

        public IQueryable<Event> GetHistoryAsync()
        {
            var oldEvents = _eventRepository.Get()
                                    .Where(@event => @event.Schedules
                                        .All(schedule => schedule.Date < DateTime.Now));

            return oldEvents;
        }

        public async Task<Event> GetAsync(long id)
        {
            return await _eventRepository.GetAsync(id);
        }
    }
}