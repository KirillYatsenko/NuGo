using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventWebScrapper.Models;
using EventWebScrapper.Repositories;
using EventWebScrapper.Services;

public class EventsCategoryService : IEventsCategoryService
{
    public readonly IEventsCategoryRepository _categoriesRepository;

    public EventsCategoryService(IEventsCategoryRepository categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }

    public IQueryable<EventCategory> Get()
    {
        return _categoriesRepository.Get();
    }

}