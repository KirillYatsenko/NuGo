using System.Threading.Tasks;
using EventWebScrapper.Controllers;
using EventWebScrapper.Enums;
using EventWebScrapper.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EventWebScrapper.Tests
{
    public class KoncertUaControllerTests
    {
        private readonly Mock<IKoncertUAService> _koncertUaScrapperService;

        public KoncertUaControllerTests()
        {
            _koncertUaScrapperService = new Mock<IKoncertUAService>();

            _koncertUaScrapperService.Setup(controller =>
                  controller.ScrapAsync(It.IsAny<EventCategories>())).ReturnsAsync(true);
        }

        [Fact]
        public async Task ScrapConcerts_ServiceScrapMethodCalledOnce()
        {
            var controller = new KoncertUAController(_koncertUaScrapperService.Object);
            await controller.ScrapConcerts();

            _koncertUaScrapperService.Verify(ctr => ctr.ScrapAsync(EventCategories.Concerts), Times.Once);
        }

        [Fact]
        public async Task ScrapConcerts_ReturnOkResponse()
        {
            var controller = new KoncertUAController(_koncertUaScrapperService.Object);
            var result = await controller.ScrapConcerts();

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task ScrapTheatre_ServiceScrapMethodCalledOnce()
        {
            var controller = new KoncertUAController(_koncertUaScrapperService.Object);
            await controller.ScrapTheatre();

            _koncertUaScrapperService.Verify(ctr => ctr.ScrapAsync(EventCategories.Theater), Times.Once);
        }

        [Fact]
        public async Task ScrapTheatre_ReturnOkResponse()
        {
            var controller = new KoncertUAController(_koncertUaScrapperService.Object);
            var result = await controller.ScrapTheatre();

            Assert.IsType<OkResult>(result);
        }

    }
}