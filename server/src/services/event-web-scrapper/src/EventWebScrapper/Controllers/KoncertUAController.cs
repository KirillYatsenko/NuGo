using System.Threading.Tasks;
using EventWebScrapper.Enums;
using EventWebScrapper.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventWebScrapper.Controllers
{
    [Route("api/[controller]")]
    public class KoncertUAController : Controller
    {
        private readonly IKoncertUAService _koncertUaService;

        public KoncertUAController(IKoncertUAService koncertUAService)
        {
            _koncertUaService = koncertUAService;
        }

        [HttpPost("concert")]
        public async Task<IActionResult> ScrapConcerts()
        {
            var scrapResult = await _koncertUaService.ScrapAsync(EventCategories.Concerts);
            return Ok();
        }

        [HttpPost("theatre")]
        public async Task<IActionResult> ScrapTheatre()
        {
            var scrapResult = await _koncertUaService.ScrapAsync(EventCategories.Theater);
            return Ok();
        }

    }

}
