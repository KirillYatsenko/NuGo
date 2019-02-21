using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventWebScrapper.Enums;
using EventWebScrapper.Models;
using EventWebScrapper.Scrappers.KoncertUAScrappers;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace EventWebScrapper.Scrappers.KoncertUAScrappers
{
    public class KoncertUAScrapper : IKoncertUAScrapper
    {
        private readonly string _koncertUaUri;
        private readonly ScrapingBrowser _browser;
        private readonly ISingletSessionScrapper _singleSessionScrapper;
        private readonly IMultipleSessionsScrapper _multipleSessionsScrapper;


        private string todayFilter
        {
            get
            {
                var today = DateTime.UtcNow;
                var todayFormated = today.ToString("yyyy-MM-dd");

                return todayFormated;
            }
        }
        public KoncertUAScrapper(IConfiguration configuration,
                                 ScrapingBrowser browser,
                                 ISingletSessionScrapper singletSessionScrapper,
                                 IMultipleSessionsScrapper multipleSessionsScrapper)
        {
            _browser = browser;
            _singleSessionScrapper = singletSessionScrapper;
            _multipleSessionsScrapper = multipleSessionsScrapper;

            _koncertUaUri = configuration["KoncertUAUri"];

            if (string.IsNullOrWhiteSpace(_koncertUaUri))
            {
                throw new ConfigurationErrorsException("KoncertUAUri entry can not be found, please check appsettings");
            }
        }

        public async Task<IEnumerable<Event>> Scrap(EventCategories category)
        {
            var scrappedEvents = new List<Event>();

            // https://concert.ua/ru/catalog/kharkiv/all-categories/date-from=2019-02-16&date-till=2019-02-17

            var uri = $"{_koncertUaUri}/ru/catalog/kyiv/{category.ToString().ToLower()}/date-from={todayFilter}&date-till={todayFilter}";

            var catalogPage = await _browser.NavigateToPageAsync(new Uri(uri));

            var performances = catalogPage.Html.CssSelect(".event").ToList();
            var multiplePerformances = catalogPage.Html.CssSelect(".selection");
            performances.AddRange(multiplePerformances);

            foreach (var performance in performances)
            {
                var newEvent = await scrapPerformance(performance, category);
                scrappedEvents.Add(newEvent);
            }

            return scrappedEvents;
        }

        private async Task<Event> scrapPerformance(HtmlNode performanceCard, EventCategories category)
        {
            var performanceUrl = scrapUrl(performanceCard);
            var detailsPage = await _browser.NavigateToPageAsync(new Uri(performanceUrl));

            var performanceTitle = scrapTitle(performanceCard);
            var description = scrapDescription(detailsPage);
            var imageUrl = scrapImageUrl(performanceCard);

            var performanceDates = scrapSessions(performanceCard, detailsPage.Html);

            var performance = new Event(performanceTitle, description, performanceUrl,
                                        imageUrl, (int)category, 0.0m, performanceDates.ToList());

            return performance;
        }

        private string scrapTitle(HtmlNode performanceCard)
        {
            var title = performanceCard.CssSelect(".event__name")
                                       .FirstOrDefault()?.InnerText;

            if (string.IsNullOrWhiteSpace(title) == true)
            {
                title = performanceCard.CssSelect(".selection__name")
                                       .FirstOrDefault()?.InnerText;
            }

            return title;
        }

        private string scrapUrl(HtmlNode performanceCard)
        {
            var relativeUrl = performanceCard.Attributes["href"]?.Value;
            var url = $"{_koncertUaUri}{relativeUrl}";

            return url;
        }

        private string scrapDescription(WebPage detailsPage)
        {
            var description = "";
            description = detailsPage.Html.CssSelect(".event-simple-text").FirstOrDefault()?.InnerText;

            return description;
        }

        private string scrapImageUrl(HtmlNode performanceHtml)
        {
            var imageUrl = performanceHtml.CssSelect(".event-info-img ")
                                            .FirstOrDefault()?.Attributes["src"].Value;

            if (imageUrl == null)
            {
                imageUrl = performanceHtml.CssSelect(".selection-img-container__img")
                                          .FirstOrDefault()?.Attributes["src"].Value;
            }

            return imageUrl;
        }

        private IEnumerable<EventDate> scrapSessions(HtmlNode performanceCard, HtmlNode detailsPage)
        {
            var eventDates = new List<EventDate>();
            var placeName = scrapPlaceName(detailsPage);
            var scrappedSessions = new List<EventDate>();

            if (eventHasMultipleDates(detailsPage))
            {
                scrappedSessions = _multipleSessionsScrapper.scrapSessions(detailsPage, performanceCard, placeName).ToList();
            }
            else
            {
                try
                {
                    scrappedSessions = _singleSessionScrapper.ScrapSession(performanceCard, detailsPage, placeName).ToList();
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("Failed to scrap session, probably because of  Ajax request");
                }
            }

            eventDates.AddRange(scrappedSessions);
            return eventDates;
        }

        private bool isSingleEvent(HtmlNode detailsPage)
        {
            return detailsPage.CssSelect(".event-buy-info__date").FirstOrDefault() != null;
        }

        private string scrapPlaceName(HtmlNode detailsPage)
        {
            var placeName = "";
            placeName = detailsPage.CssSelect(".event__place").FirstOrDefault()?
                                         .InnerText;

            if (string.IsNullOrWhiteSpace(placeName) == true)
            {
                placeName = detailsPage.CssSelect(".event-tickets-item-info__description")
                                        .ToArray()[1].InnerText;
            }

            return placeName;
        }

        private bool eventHasMultipleDates(HtmlNode detailsPage)
        {
            Thread.Sleep(500);
            var events = detailsPage.CssSelect(".event-tickets").FirstOrDefault();
            var theatreEvents = detailsPage.CssSelect(".event-tickets-item-theatre").FirstOrDefault();
            return events != null || theatreEvents != null;
        }

    }
}