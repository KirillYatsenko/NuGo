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
        private readonly IEventRepository _eventRepository;
        private readonly IEventDateRepository _eventDateRepository;

        public ScrapperService(
            IScrapper scrapper,
            IEventRepository eventRepository,
            IEventDateRepository eventDateRepository
        )
        {
            _eventRepository = eventRepository;
            _eventDateRepository = eventDateRepository;
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

            var eventDates = _eventDateRepository.Get();

            var todayEventDates = await eventDates
                                    .Where(d => d.Date.Day == filterDay && d.Event.Id == eventToRemove.Id)
                                    .ToListAsync();

            await _eventDateRepository.RemoveRangeAsync(todayEventDates);
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