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

namespace EventWebScrapper.Tests
{
    public class KinoAfishaScrapperServiceTests
    {
        private readonly Mock<IKinoAfishaScrapper> _kinoAfishaScrapperMock;
        private readonly Mock<IEventRepository> _eventRepositoryMock;
        private readonly Mock<IEventDateRepository> _eventDateRepositoryMock;

        public KinoAfishaScrapperServiceTests()
        {
            _kinoAfishaScrapperMock = new Mock<IKinoAfishaScrapper>();
            _eventRepositoryMock = new Mock<IEventRepository>();
            _eventDateRepositoryMock = new Mock<IEventDateRepository>();
        }

        [Fact]
        public async Task ScrapAsync_EventDateRepositoryGetCalledOnce()
        {
            var service = new KinoAfishaScrapperService(_eventRepositoryMock.Object,
                                                        _eventDateRepositoryMock.Object,
                                                        _kinoAfishaScrapperMock.Object);

            await service.ScrapAsync();

            _eventDateRepositoryMock.Verify(sr => sr.Get(), Times.Once);
        }

        [Fact]
        public async Task ScrapAsync_KinoAfishaScrapperScrapCalledOnce()
        {
            var service = new KinoAfishaScrapperService(_eventRepositoryMock.Object,
                                                        _eventDateRepositoryMock.Object,
                                                        _kinoAfishaScrapperMock.Object);

            await service.ScrapAsync();

            _kinoAfishaScrapperMock.Verify(sr => sr.Scrap(), Times.Once);
        }

        [Fact]
        public async Task ScrapAsync_EventRepositoryAddRangeCalledOnce()
        {
            var service = new KinoAfishaScrapperService(_eventRepositoryMock.Object,
                                                        _eventDateRepositoryMock.Object,
                                                        _kinoAfishaScrapperMock.Object);

            await service.ScrapAsync();

            _eventRepositoryMock.Verify(sr => sr.AddRangeAsync(It.IsAny<IEnumerable<Event>>()), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public async Task ScrapAsync_ScrapperReturnedEvents_AddedToRepository(int eventsCount)
        {
            var scrappedEvents = generateEvents(eventsCount);
            _kinoAfishaScrapperMock.Setup(scr => scr.Scrap()).ReturnsAsync(scrappedEvents);

            var eventsEmpty = new List<Event>();
            var eventsMock = eventsEmpty.AsQueryable().BuildMock();
            _eventRepositoryMock.Setup(rp => rp.Get()).Returns(eventsMock.Object);

            var service = new KinoAfishaScrapperService(_eventRepositoryMock.Object,
                                                        _eventDateRepositoryMock.Object,
                                                        _kinoAfishaScrapperMock.Object);

            await service.ScrapAsync();

            _eventRepositoryMock.Verify(sr => sr.AddRangeAsync(scrappedEvents), Times.Once);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public async Task ScrapAsync_EventsAlreadyExistInRepository_RemoveAllOldEventsFromRepositoryAddNewOnes(int eventsCount)
        {
            var scrappedEvents = generateEvents(eventsCount);
            _kinoAfishaScrapperMock.Setup(scr => scr.Scrap()).ReturnsAsync(scrappedEvents);

            var eventsMock = scrappedEvents.AsQueryable().BuildMock();
            _eventRepositoryMock.Setup(rp => rp.Get()).Returns(eventsMock.Object);

            var todaysEvents = new List<EventDate>();
            var todaysEventsMock = todaysEvents.AsQueryable().BuildMock();
            _eventDateRepositoryMock.Setup(rp => rp.Get()).Returns(todaysEventsMock.Object);

            var service = new KinoAfishaScrapperService(_eventRepositoryMock.Object,
                                                        _eventDateRepositoryMock.Object,
                                                        _kinoAfishaScrapperMock.Object);

            await service.ScrapAsync();

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