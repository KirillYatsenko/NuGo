using System;
using Xunit;
using System.Threading.Tasks;
using EventWebScrapper.Controllers;

namespace EventWebScrapper.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1Async()
        {
            var controller = new  KinoAfishaController(null);
            await controller.Scrap();
        }
    }
}
