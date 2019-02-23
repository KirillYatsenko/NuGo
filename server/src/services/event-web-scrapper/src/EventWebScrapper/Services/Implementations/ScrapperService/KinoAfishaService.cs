using System;
using System.Linq;
using System.Threading.Tasks;
using EventWebScrapper.Enums;
using EventWebScrapper.Models;
using EventWebScrapper.Repositories;
using EventWebScrapper.Scrappers;
using Microsoft.EntityFrameworkCore;

namespace EventWebScrapper.Services
{
    public class KinoAfishaService : ScrapperService, IKinoAfishaService
    {
        public KinoAfishaService(IKinoAfishaScrapper scrapper,
                                 IEventRepository eventRepository,
                                 IEventDateRepository eventDateRepository)

            : base(scrapper, eventRepository, eventDateRepository) { }
    }
}