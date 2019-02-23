using EventWebScrapper.Repositories;
using EventWebScrapper.Scrappers;
using EventWebScrapper.Services;

namespace EventWebScrapper.Tests.Mocks
{
    public class ScrapperServiceMock : ScrapperService
    {
        public ScrapperServiceMock(IScrapper scrapper,
                                   IEventRepository eventRepository,
                                   IEventDateRepository eventDateRepository) : base(scrapper, eventRepository, eventDateRepository)
        { }

    }
}