using System.Collections.Generic;
using EventWebScrapper.Models;
using HtmlAgilityPack;

namespace EventWebScrapper.Scrappers.KoncertUAScrappers
{
    public interface IMultipleSessionsScrapper
    {
        IEnumerable<EventDate> scrapSessions(HtmlNode detailsPage, HtmlNode performanceCard, 
                                             string placeName);
    }
}