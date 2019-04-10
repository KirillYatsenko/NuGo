using System.Linq;
using System.Threading.Tasks;
using EventWebScrapper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventWebScrapper
{
    [Route("api/[controller]")]
    public class EventsController
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRelevant()
        {
            var events = await _eventService.GetRelevantAsync();
            return new OkObjectResult(events);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetOld()
        {
            var events = await _eventService.GetHistoryAsync().ToListAsync();
            return new OkObjectResult(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var @event = await _eventService.GetAsync(id);
            return new OkObjectResult(@event);
        }

    }
}