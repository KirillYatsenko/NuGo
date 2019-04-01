using EventWebScrapper.Repositories;
using EventWebScrapper.Scrappers.KoncertUAScrappers;

namespace EventWebScrapper.Services
{
    public class KoncertUAService : ScrapperService, IKoncertUAService
    {
        public KoncertUAService(IKoncertUAScrapper scrapper, 
                                IEventsRepository eventRepository, 
                                IEventsScheduleRepository eventDateRepository)
               : base(scrapper, eventRepository, eventDateRepository) { }
    }

}