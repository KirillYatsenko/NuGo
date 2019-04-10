using System.Collections.Generic;
using System.Threading.Tasks;
using AdminPageMVC.ViewModels;

namespace AdminPageMVC.Services
{
    public interface IEventsService
    {
        Task<IEnumerable<Event>> GetRelevantAsync();
        Task<IEnumerable<Event>> GetHistoryAsync();
        Task<Event> GetAsync(long id);
    }
}