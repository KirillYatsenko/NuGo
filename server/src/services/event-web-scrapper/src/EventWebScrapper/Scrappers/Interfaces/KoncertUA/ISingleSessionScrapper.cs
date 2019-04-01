using System.Collections.Generic;
using EventWebScrapper.Models;
using HtmlAgilityPack;

namespace EventWebScrapper.Scrappers.KoncertUAScrappers
{
    public interface ISingletSessionScrapper
    {
        IEnumerable<EventSchedule> ScrapSession(HtmlNode performanceCard, HtmlNode performance, string placeName);
    }
}