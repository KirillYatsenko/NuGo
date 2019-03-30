using System.Collections.Generic;
using System.Threading.Tasks;
using AdminPageMVC.Models;

namespace AdminPageMVC.Services
{
    public interface IEventsService
    {
        Task<IEnumerable<Event>> GetAsync();
        Task<Event> GetAsync(long id);
    }
}