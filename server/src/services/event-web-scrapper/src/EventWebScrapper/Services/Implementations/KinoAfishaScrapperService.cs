using System;
using System.Linq;
using System.Threading.Tasks;
using EventWebScrapper.Models;
using EventWebScrapper.Repositories;
using EventWebScrapper.Scrappers;
using Microsoft.EntityFrameworkCore;

namespace EventWebScrapper.Services
{
    public class KinoAfishaScrapperService : IKinoAfishaScrapperService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEventDateRepository _eventDateRepository;
        private readonly IKinoAfishaScrapper _kinoAfishaScrapper;

        public KinoAfishaScrapperService(
            IEventRepository eventRepository,
            IEventDateRepository eventDateRepository,
            IKinoAfishaScrapper kinoAfishaScrapper
        )
        {
            _eventRepository = eventRepository;
            _eventDateRepository = eventDateRepository;
            _kinoAfishaScrapper = kinoAfishaScrapper;
        }

        public async Task<bool> ScrapAsync()
        {
            var todaysDay = DateTime.UtcNow.Day;
            var scrappedEvents = await _kinoAfishaScrapper.Scrap();

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