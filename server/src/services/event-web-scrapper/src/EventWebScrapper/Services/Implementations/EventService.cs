using System.Linq;
using System.Threading.Tasks;
using EventWebScrapper.Models;
using EventWebScrapper.Repositories;

namespace EventWebScrapper.Services
{
    public class EventService : IEventService
    {
        private readonly IEventsRepository _eventRepository;

        public EventService(IEventsRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public IQueryable<Event> Get()
        {
            return _eventRepository.Get();
        }

        public async Task<Event> GetAsync(long id)
        {
            return await _eventRepository.GetAsync(id);
        }
    }
}