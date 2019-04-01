using EventWebScrapper.Repositories;
using EventWebScrapper.Scrappers.KinoAfishaScrappers;

namespace EventWebScrapper.Services
{
    public class KinoAfishaService : ScrapperService, IKinoAfishaService
    {
        public KinoAfishaService(IKinoAfishaScrapper scrapper,
                                 IEventsRepository eventRepository,
                                 IEventsScheduleRepository eventDateRepository)

            : base(scrapper, eventRepository, eventDateRepository) { }
    }
}