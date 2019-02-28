using EventWebScrapper.Repositories;
using EventWebScrapper.Scrappers.KoncertUAScrappers;

namespace EventWebScrapper.Services
{
    public class KoncertUAService : ScrapperService, IKoncertUAService
    {
        public KoncertUAService(IKoncertUAScrapper scrapper, 
                                IEventRepository eventRepository, 
                                IEventDateRepository eventDateRepository)
               : base(scrapper, eventRepository, eventDateRepository) { }
    }

}