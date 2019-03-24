using System.Threading.Tasks;
using EventBus.Abstracts;
using EventWebScrapper.IntegrationEvents;
using EventWebScrapper.Services;

public class KoncertUaEventHandler : IEventHandler<ScrapIntegrationEvent>
{
    private readonly IKoncertUAService _koncertUaService;

    public KoncertUaEventHandler(IKoncertUAService koncertUaService)
    {
        _koncertUaService = koncertUaService;
    }

    public async Task Handle(ScrapIntegrationEvent message)
    {
        await _koncertUaService.ScrapAsync(message.Category);
    }
}