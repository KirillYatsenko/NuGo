using System.Threading.Tasks;
using EventBus.Abstracts;
using EventWebScrapper.IntegrationEvents;
using EventWebScrapper.Services;

namespace EventWebScrapper.EventHandlers
{
    public class ScrapKinoafishaEventHandler: IEventHandler<ScrapIntegrationEvent>
    {
        private readonly IKinoAfishaService _kinoAfishaService;

        public ScrapKinoafishaEventHandler(IKinoAfishaService kinoAfishaService)
        {
            _kinoAfishaService = kinoAfishaService;
        }

        public  async Task Handle(ScrapIntegrationEvent message)
        {
            await _kinoAfishaService.ScrapAsync(message.Category);
        }
    }
}