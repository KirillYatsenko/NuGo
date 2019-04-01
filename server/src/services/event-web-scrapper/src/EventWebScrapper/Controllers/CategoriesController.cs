using System.Threading.Tasks;
using EventWebScrapper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventWebScrapper.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController
    {
        private readonly IEventsCategoryService _categoryService;

        public CategoriesController(IEventsCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.Get().ToListAsync();
            return new OkObjectResult(categories);
        }

    }
}