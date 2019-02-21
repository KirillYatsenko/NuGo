using System;
using System.Collections.Generic;
using System.Linq;
using EventWebScrapper.Models;
using HtmlAgilityPack;
using ScrapySharp.Extensions;

namespace EventWebScrapper.Scrappers.KoncertUAScrappers
{
    public class SingletSessionScrapper : ISingletSessionScrapper
    {
        public IEnumerable<EventDate> ScrapSession(HtmlNode performanceCard, HtmlNode performance, string placeName)
        {
            var sessions = new List<EventDate>();

            var session = scrapSession(performanceCard, performance, placeName);
            sessions.Add(session);

            return sessions;
        }


        private EventDate scrapSession(HtmlNode performanceCard, HtmlNode performance, string placeName)
        {
            var performanceDate = new EventDate();

            var address = scrapAddress(performance);
            var date = scrapDate(performance);
            var price = scrapPrice(performanceCard);

            performanceDate.HostName = placeName;
            performanceDate.Address = address;
            performanceDate.Date = date;
            performanceDate.Price = price;

            return performanceDate;
        }

        private string scrapAddress(HtmlNode detailsPage)
        {
            var address = "";
            address = detailsPage.CssSelect("#venue-address")
                                      .FirstOrDefault()?.InnerText;

            return address;
        }

        private DateTime scrapDate(HtmlNode detailsPage)
        {
            var sessionDate = new DateTime();

            var timeText = detailsPage.CssSelect(".event-buy-info__text")
                                           .FirstOrDefault()?.InnerText;

            sessionDate = DateTime.ParseExact(timeText, "HH:mm",
                    System.Globalization.CultureInfo.InvariantCulture);

            return sessionDate;
        }

        private string scrapPrice(HtmlNode performanceCard)
        {
            var price = "";

            price = performanceCard.CssSelect(".event__price")
                                    .FirstOrDefault()?.InnerText;

            return price;
        }



    }
}