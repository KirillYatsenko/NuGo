using System.Linq;
using System.Threading.Tasks;
using EventWebScrapper.Repositories;
using EventWebScrapper.Scrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventWebScrapper.Controllers
{
    [Route("api/[controller]")]
    public class KinoAfishaController : Controller
    {
        private readonly IEventScrapDataRepository _eventScrapDataRepository;
        private readonly IKinoAfishaScrapper _kinoAfishaScrapper;

        public KinoAfishaController(
            IEventScrapDataRepository eventScrapDataRepository,
            IKinoAfishaScrapper kinoAfishaScrapper)
        {
            _eventScrapDataRepository = eventScrapDataRepository;
            _kinoAfishaScrapper = kinoAfishaScrapper;
        }

        [HttpPost]
        public async Task<IActionResult> Invoke()
        {
            await _kinoAfishaScrapper.Scrap();
            return null;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var scrapData = await _eventScrapDataRepository.Get().ToListAsync();
            return Ok(scrapData);
        }

    }

}
