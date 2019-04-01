using System;
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

            foreach (var scrappedEvent in scrappedEvents)
            {
                var existingEvent = await checkEventExists(scrappedEvent);
                await removeExistingEvent(existingEvent, todaysDay);
            }

            return await this._eventRepository.AddRangeAsync(scrappedEvents);
        }

        private async Task removeExistingEvent(Event eventToRemove, int filterDay)
        {
            if (eventToRemove == null)
            {
                return;
            }

            var eventDates = _eventsScheduleRepository.Get();

            var todayEventDates = await eventDates
                                    .Where(d => d.Date.Day == filterDay && d.Event.Id == eventToRemove.Id)
                                    .ToListAsync();

            await _eventsScheduleRepository.RemoveRangeAsync(todayEventDates);
            await _eventRepository.RemoveAsync(eventToRemove.Id);
        }

        private async Task<Event> checkEventExists(Event eventInfo)
        {
            var events = _eventRepository.Get();
            var eventAlreadyExists = await events.FirstOrDefaultAsync(e => e.Title == eventInfo.Title);

            return eventAlreadyExists;
        }

    }
}