using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventWebScrapper.Enums;
using EventWebScrapper.Models;
using EventWebScrapper.Repositories;
using EventWebScrapper.Scrappers;
using Microsoft.EntityFrameworkCore;

namespace EventWebScrapper.Services
{
    public abstract class ScrapperService
    {
        private readonly IScrapper _scrapper;
        private readonly IEventsRepository _eventRepository;
        private readonly IEventsScheduleRepository _eventsScheduleRepository;

        public ScrapperService(
            IScrapper scrapper,
            IEventsRepository eventRepository,
            IEventsScheduleRepository eventDateRepository
        )
        {
            _eventRepository = eventRepository;
            _eventsScheduleRepository = eventDateRepository;
            _scrapper = scrapper;
        }

        public async Task<bool> ScrapAsync(EventCategories category)
        {
            var todaysDay = DateTime.UtcNow.Day;
            var scrappedEvents = await _scrapper.Scrap(category);
            var newEvents = scrappedEvents.ToList();

            var uniqueEvents = scrappedEvents.ToList();

            foreach (var scrappedEvent in scrappedEvents)
            {
                var existingEvent = await checkEventExistsByTitle(scrappedEvent);

                if (await addNewSchedules(existingEvent, scrappedEvent.Schedules))
                {
                    newEvents.Remove(scrappedEvent);
                }
            }

            return await this._eventRepository.AddRangeAsync(newEvents);
        }

        private async Task<Event> checkEventExistsByTitle(Event eventInfo)
        {
            var events = _eventRepository.GetWithSchedules();
            var existedEvent = await events.FirstOrDefaultAsync(e => e.Title == eventInfo.Title);

            return existedEvent;
        }

        private async Task<bool> addNewSchedules(Event existingEvent, List<EventSchedule> newSchedules)
        {
            if (existingEvent == null)
            {
                return false;
            }

                foreach (var schedule in newSchedules)
                {
                if (!existingEvent.Schedules.Exists(sch => sch.Date != schedule.Date))
                {
                    existingEvent.Schedules.Add(schedule);
                }
            }

            return await _eventRepository.UpdateAsync(existingEvent);
        }

    }
}