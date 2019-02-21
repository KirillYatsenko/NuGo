using System.Linq;
using System.Threading.Tasks;
using EventWebScrapper.Enums;
using EventWebScrapper.Repositories;
using EventWebScrapper.Scrappers;
using EventWebScrapper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventWebScrapper.Controllers
{
    [Route("api/[controller]")]
    public class KinoAfishaController : Controller
    {
        private readonly IKinoAfishaService _kinoAfishaService;

        public KinoAfishaController(IKinoAfishaService kinoAfishaService)
        {
            _kinoAfishaService = kinoAfishaService;
        }

        [HttpPost]
        public async Task<IActionResult> Scrap()
        {
            var scrapResult = await _kinoAfishaService.ScrapAsync(EventCategories.Cinema);
            return Ok();
        }

    }

}
