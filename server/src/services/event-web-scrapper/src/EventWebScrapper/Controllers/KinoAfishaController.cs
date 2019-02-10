using System.Linq;
using System.Threading.Tasks;
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
        private readonly IKinoAfishaScrapperService _kinoAfishaScrapper;

        public KinoAfishaController(IKinoAfishaScrapperService kinoAfishaScrapper)
        {
            _kinoAfishaScrapper = kinoAfishaScrapper;
        }

        [HttpPost]
        public async Task<IActionResult> Scrap()
        {
            var scrapResult = await _kinoAfishaScrapper.ScrapAsync();
            return Ok("scrapped");
        }

    }

}
