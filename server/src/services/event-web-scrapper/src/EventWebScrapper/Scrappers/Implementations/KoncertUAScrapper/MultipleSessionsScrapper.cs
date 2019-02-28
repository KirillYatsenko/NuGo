using System;
using System.Collections.Generic;
using System.Linq;
using EventWebScrapper.Models;
using HtmlAgilityPack;
using ScrapySharp.Extensions;

namespace EventWebScrapper.Scrappers.KoncertUAScrappers
{
    public class MultipleSessionsScrapper : IMultipleSessionsScrapper
    {
        public IEnumerable<EventDate> scrapSessions(HtmlNode detailsPage, HtmlNode performanceCard,
                                                    string placeName)
        {
            var scrappedSessions = new List<EventDate>();
            scrappedSessions = scrapMultipleSessions(detailsPage, performanceCard, placeName).ToList();

            return scrappedSessions;
        }

        private IEnumerable<EventDate> scrapMultipleSessions(HtmlNode detailsPage, HtmlNode performanceCard, string placeName)
        {
            var sessions = new List<EventDate>();
            var tickets = detailsPage.CssSelect(".event-tickets-item");

            foreach (var ticket in tickets)
            {
                var session = scrapSession(performanceCard, ticket, placeName);
                sessions.Add(session);
            }

            return sessions;
        }

        private EventDate scrapSession(HtmlNode performanceCard, HtmlNode ticket, string placeName)
        {
            var performanceDate = new EventDate();

            var address = scrapAddress(ticket);
            var date = scrapDate(ticket);
            var price = scrapPrice(ticket);

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

        private DateTime scrapDate(HtmlNode ticket)
        {
            var ticketDate = new DateTime();

            var timeText = ticket.CssSelect(".event-tickets-item-info__description")
                                        .FirstOrDefault()?.InnerText;

            timeText = timeText.Replace("\n", "");

            if (timeText[0] == ' ')
            {
                timeText.Remove(0, 1);
            }

            timeText = MonthsConvertor.ReplaceMonth(timeText);

            // Ex.: 22 февраля 2019 18:00
            ticketDate = DateTime.ParseExact(timeText, "d MM yyyy HH:mm ",
                                  System.Globalization.CultureInfo.InvariantCulture);

            return ticketDate;
        }

        private PriceInfo scrapPrice(HtmlNode performanceCard)
        {
            var price = performanceCard.CssSelect(".event-tickets-item-buy__price-value")
                              .FirstOrDefault()?.InnerText;

            var priceInfo = KoncertUaPriceConvertor.Convert(price);

            return priceInfo;
        }

    }
}