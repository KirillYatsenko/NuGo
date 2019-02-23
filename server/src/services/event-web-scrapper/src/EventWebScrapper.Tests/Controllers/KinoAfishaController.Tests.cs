using System;
using Xunit;
using System.Threading.Tasks;
using EventWebScrapper.Controllers;
using Moq;
using EventWebScrapper.Services;
using Microsoft.AspNetCore.Mvc;
using EventWebScrapper.Enums;

namespace EventWebScrapper.Tests
{
    public class KinoAfishaControllerTests
    {
        private readonly Mock<IKinoAfishaService> _kinoAfishaScrapperService;

        public KinoAfishaControllerTests()
        {
            _kinoAfishaScrapperService = new Mock<IKinoAfishaService>();

            _kinoAfishaScrapperService.Setup(controller =>
                    controller.ScrapAsync(It.IsAny<EventCategories>())).ReturnsAsync(true);
        }

        [Fact]
        public async Task Scrap_ServiceScrapMethodCalledOnce()
        {
            var controller = new KinoAfishaController(_kinoAfishaScrapperService.Object);
            await controller.Scrap();

            _kinoAfishaScrapperService.Verify(ctr => ctr.ScrapAsync(EventCategories.Cinema), Times.Once);
        }

        [Fact]
        public async Task Scrap_ReturnOkResponse()
        {
            var controller = new KinoAfishaController(_kinoAfishaScrapperService.Object);
            var result = await controller.Scrap();

            Assert.IsType<OkResult>(result);
        }
    }
}
