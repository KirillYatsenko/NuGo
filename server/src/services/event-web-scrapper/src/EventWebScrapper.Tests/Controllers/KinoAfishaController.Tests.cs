using System;
using Xunit;
using System.Threading.Tasks;
using EventWebScrapper.Controllers;
using Moq;
using EventWebScrapper.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventWebScrapper.Tests
{
    public class KinoAfishaControllerTests
    {
        private readonly Mock<IKinoAfishaService> _kinoAfishaControllerMock;

        public KinoAfishaControllerTests()
        {
            _kinoAfishaControllerMock = new Mock<IKinoAfishaService>();
            _kinoAfishaControllerMock.Setup(controller => controller.ScrapAsync()).ReturnsAsync(true);
        }

        [Fact]
        public async Task Scrap_ServiceScrapMethodCalledOnce()
        {
            var controller = new KoncertUAController(_kinoAfishaControllerMock.Object);
            await controller.ScrapConcerts();

            _kinoAfishaControllerMock.Verify(ctr => ctr.ScrapAsync(), Times.Once);
        }

        [Fact]
        public async Task Scrap_ReturnOkResponse()
        {
            var controller = new KoncertUAController(_kinoAfishaControllerMock.Object);
            var result = await controller.ScrapConcerts();
            
            Assert.IsType<OkResult>(result);
        }
    }
}
