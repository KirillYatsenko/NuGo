using System.Collections.Generic;
using EventWebScrapper.Models;
using HtmlAgilityPack;

namespace EventWebScrapper.Scrappers.KoncertUAScrappers
{
    public interface IMultipleSessionsScrapper
    {
        IEnumerable<EventSchedule> scrapSessions(HtmlNode detailsPage, HtmlNode performanceCard, 
                                             string placeName);
    }
}