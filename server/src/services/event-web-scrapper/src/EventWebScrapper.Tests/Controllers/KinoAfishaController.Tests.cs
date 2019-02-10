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
        private readonly Mock<IKinoAfishaScrapperService> _kinoAfishaControllerMock;

        public KinoAfishaControllerTests()
        {
            _kinoAfishaControllerMock = new Mock<IKinoAfishaScrapperService>();
            _kinoAfishaControllerMock.Setup(controller => controller.ScrapAsync()).ReturnsAsync(true);
        }

        [Fact]
        public async Task Scrap_ServiceScrapMethodCalledOnce()
        {
            var controller = new KinoAfishaController(_kinoAfishaControllerMock.Object);
            await controller.Scrap();

            _kinoAfishaControllerMock.Verify(ctr => ctr.ScrapAsync(), Times.Once);
        }

        [Fact]
        public async Task Scrap_ReturnOkResponse()
        {
            var controller = new KinoAfishaController(_kinoAfishaControllerMock.Object);
            var result = await controller.Scrap();
            
            Assert.IsType<OkResult>(result);
        }
    }
}
