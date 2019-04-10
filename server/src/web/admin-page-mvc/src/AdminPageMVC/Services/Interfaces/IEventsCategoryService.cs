using System.Collections.Generic;
using System.Threading.Tasks;
using AdminPageMVC.ViewModels;

namespace AdminPageMVC.Services
{
    public interface IEventsCategoryService
    {
        Task<IEnumerable<EventCategory>> GetAsync();
    }

}
