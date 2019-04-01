using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventWebScrapper.Models;
using EventWebScrapper.Repositories;
using EventWebScrapper.Scrappers;
using EventWebScrapper.Services;
using Moq;
using Xunit;
using MockQueryable.Moq;
using EventWebScrapper.Tests.Mocks;
using EventWebScrapper.Enums;

namespace EventWebScrapper.Tests
{
    public class ScrapperServiceTests
    {
        private readonly Mock<IScrapper> _scrapperMock;
        private readonly Mock<IEventsRepository> _eventRepositoryMock;
        private readonly Mock<IEventsScheduleRepository> _eventDateRepositoryMock;

        public ScrapperServiceTests()
        {
            _scrapperMock = new Mock<IScrapper>();
            _eventRepositoryMock = new Mock<IEventsRepository>();
            _eventDateRepositoryMock = new Mock<IEventsScheduleRepository>();
        }

        [Theory]
        [InlineData(EventCategories.Cinema)]
        [InlineData(EventCategories.Theater)]
        [InlineData(EventCategories.Concerts)]
        public async Task ScrapAsync_KinoAfishaScrapperScrapCalledOnce(EventCategories category)
        {
            var serviceMock = new ScrapperServiceMock(_scrapperMock.Object,
                                                        _eventRepositoryMock.Object,
                                                        _eventDateRepositoryMock.Object);

            await serviceMock.ScrapAsync(category);

            _scrapperMock.Verify(sr => sr.Scrap(category), Times.Once);
        }

        [Theory]
        [InlineData(EventCategories.Cinema)]
        [InlineData(EventCategories.Theater)]
        [InlineData(EventCategories.Concerts)]
        public async Task ScrapAsync_EventRepositoryAddRangeCalledOnce(EventCategories category)
        {
            var serviceMock = new ScrapperServiceMock(_scrapperMock.Object,
                                                         _eventRepositoryMock.Object,
                                                         _eventDateRepositoryMock.Object);

            await serviceMock.ScrapAsync(category);

            _eventRepositoryMock.Verify(sr => sr.AddRangeAsync(It.IsAny<IEnumerable<Event>>()), Times.Once);
        }

        [Theory]
        [InlineData(1, EventCategories.Cinema)]
        [InlineData(10, EventCategories.Theater)]
        [InlineData(100, EventCategories.Concerts)]
        public async Task ScrapAsync_ScrapperReturnedEvents_AddedToRepository(int eventsCount, EventCategories category)
        {
            var scrappedEvents = generateEvents(eventsCount);
            _scrapperMock.Setup(scr => scr.Scrap(category)).ReturnsAsync(scrappedEvents);

            var eventsEmpty = new List<Event>();
            var eventsMock = eventsEmpty.AsQueryable().BuildMock();
            _eventRepositoryMock.Setup(rp => rp.Get()).Returns(eventsMock.Object);

            var serviceMock = new ScrapperServiceMock(_scrapperMock.Object,
                                                        _eventRepositoryMock.Object,
                                                        _eventDateRepositoryMock.Object);

            await serviceMock.ScrapAsync(category);

            _eventRepositoryMock.Verify(sr => sr.AddRangeAsync(scrappedEvents), Times.Once);
        }


        [Theory]
        [InlineData(1, EventCategories.Cinema)]
        [InlineData(10, EventCategories.Theater)]
        [InlineData(100, EventCategories.Concerts)]
        public async Task ScrapAsync_EventsAlreadyExistInRepository_RemoveAllOldEventsFromRepositoryAddNewOnes(int eventsCount, EventCategories category)
        {
            var scrappedEvents = generateEvents(eventsCount);
            _scrapperMock.Setup(scr => scr.Scrap(category)).ReturnsAsync(scrappedEvents);

            var eventsMock = scrappedEvents.AsQueryable().BuildMock();
            _eventRepositoryMock.Setup(rp => rp.Get()).Returns(eventsMock.Object);

            var todaysEvents = new List<EventSchedule>();
            var todaysEventsMock = todaysEvents.AsQueryable().BuildMock();
            _eventDateRepositoryMock.Setup(rp => rp.Get()).Returns(todaysEventsMock.Object);

            var serviceMock = new ScrapperServiceMock(_scrapperMock.Object,
                                                         _eventRepositoryMock.Object,
                                                         _eventDateRepositoryMock.Object);

            await serviceMock.ScrapAsync(category);

            foreach (var scrappedEvent in scrappedEvents)
            {
                _eventRepositoryMock.Verify(sr => sr.RemoveAsync(scrappedEvent.Id), Times.Once);
            }
        }

        private IEnumerable<Event> generateEvents(int count)
        {
            var events = new List<Event>();

            for (int i = 0; i < count; i++)
            {
                var newEvent = new Event()
                {
                    Id = i,
                    Title = $"title#{i}",
                    Rating = i
                };

                events.Add(newEvent);
            }

            return events;
        }

    }
}