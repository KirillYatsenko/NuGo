using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventWebScrapper.Enums;
using EventWebScrapper.Models;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace EventWebScrapper.Scrappers.KinoAfishaScrappers
{
    public class KinoAfishaScrapper : IKinoAfishaScrapper
    {
        private readonly string _kinoAfishaUri;
        private readonly ScrapingBrowser _browser;

        private readonly IEventImageScrapper _eventImageScrapper;

        public KinoAfishaScrapper(IConfiguration configuration,
                                  IEventImageScrapper eventImageScrapper,
                                  ScrapingBrowser browser)
        {
            _browser = browser;
            _eventImageScrapper = eventImageScrapper;
            _kinoAfishaUri = configuration["KinoAfishaUri"];

            if (string.IsNullOrWhiteSpace(_kinoAfishaUri))
            {
                throw new ConfigurationErrorsException("KinoAfishaUri entry can not be found, please check appsettings");
            }
        }

        public async Task<IEnumerable<Event>> Scrap(EventCategories category)
        {
            var scrappedEvents = new List<Event>();

            var kinoafishaHomePage = await _browser.NavigateToPageAsync(new Uri($"{_kinoAfishaUri}/kinoafisha"));
            var films = kinoafishaHomePage.Html.CssSelect(".list-films").First()?.CssSelect(".item > .text > h3");

            foreach (var film in films)
            {
                var newEvent = await scrapFilm(film, category);
                scrappedEvents.Add(newEvent);
            }

            return scrappedEvents;
        }

        private async Task<Event> scrapFilm(HtmlNode filmHtml, EventCategories category)
        {
            var filmUrl = scrapFilmUrl(filmHtml);
            var detailsPage = await _browser.NavigateToPageAsync(new Uri(filmUrl));

            var rating = scrapFilmRating(detailsPage);
            var description = scrapFilmDescription(detailsPage);
            var imageUrl = await scrapImageUrl(detailsPage, filmHtml.InnerText);

            var sessions = scrapSessions(detailsPage);

            var film = new Event(filmHtml.InnerText, description, filmUrl,
                                 imageUrl, (int)category, rating, sessions);

            return film;
        }

        private string scrapFilmUrl(HtmlNode filmHtml)
        {
            var url = "";

            var relativeUrl = filmHtml.CssSelect("a")?.First().Attributes["href"].Value;
            url = $"{_kinoAfishaUri}{relativeUrl}";

            return url;
        }

        private decimal scrapFilmRating(WebPage filmDetailsPage)
        {
            var rating = 0.0M;

            var ratingString = filmDetailsPage.Html.CssSelect("span[itemprop=ratingValue]").FirstOrDefault().InnerText;
            rating = Decimal.Parse(ratingString.Replace(',', '.'));

            return rating;
        }

        private string scrapFilmDescription(WebPage filmDetailsPage)
        {
            var description = "";
            description = filmDetailsPage.Html.CssSelect(".description").FirstOrDefault()?.InnerHtml;

            return description;
        }

        private async Task<string> scrapImageUrl(WebPage detailsPage, string eventTitle)
        {
            var imageUrl = "";

            var relativePhotoUrl = detailsPage.Html.CssSelect(".photo").FirstOrDefault()?
                                                   .Attributes["href"].Value;

            imageUrl = $"{_kinoAfishaUri}{relativePhotoUrl}";

            var imagePath = await _eventImageScrapper.ScrapImage(imageUrl, eventTitle);

            return imagePath;
        }

        private List<EventSchedule> scrapSessions(WebPage detailsPage)
        {
            var sessions = new List<EventSchedule>();

            var cinemaSchedules = detailsPage.Html.CssSelect(".film-sessions");

            foreach (var session in cinemaSchedules)
            {
                List<EventSchedule> cinemaSessions = scrapCinemaSessions(session);
                sessions.AddRange(cinemaSessions);
            }

            return sessions;
        }

        private List<EventSchedule> scrapCinemaSessions(HtmlNode session)
        {
            var sessions = new List<EventSchedule>();

            var cinemaInfo = session.CssSelect("tr").FirstOrDefault();

            var cinemaName = scrapCinemaName(cinemaInfo);
            var address = scrapCinemaAddress(cinemaInfo);

            var cinemaHalls = session.CssSelect("tr").Skip(1);

            foreach (var cinemaHall in cinemaHalls)
            {
                var cinemaHallEvents = scrapCinemaHallDates(cinemaHall, cinemaName, address);
                sessions.AddRange(cinemaHallEvents);
            }

            return sessions;
        }

        private string scrapCinemaName(HtmlNode cinemaInfo)
        {
            var cinemaName = "";
            cinemaName = cinemaInfo.CssSelect(".cinema-room > a > b").FirstOrDefault().InnerText;

            return cinemaName;
        }

        private string scrapCinemaAddress(HtmlNode cinemaInfo)
        {
            var cinemaAddress = "";
            cinemaAddress = cinemaInfo.CssSelect(".cinema-room > a").FirstOrDefault().Attributes["title"].Value;

            return cinemaAddress;
        }

        private List<EventSchedule> scrapCinemaHallDates(HtmlNode cinemaHall, string cinemaName, string cinemaAddress)
        {
            var hallSessions = new List<EventSchedule>();

            var hallPrice = scrapCinemaHallPrice(cinemaHall);

            hallSessions = scrapHallSessions(cinemaHall);

            hallSessions.ForEach(session =>
            {
                session.HostName = cinemaName;
                session.Address = cinemaAddress;
                session.Price = hallPrice;
            });

            return hallSessions;
        }

        private PriceInfo scrapCinemaHallPrice(HtmlNode cinemaHall)
        {
            var price = cinemaHall.CssSelect(".dotted > .note").FirstOrDefault()?.InnerText;

            var priceInfo = KinoAfishaPriceConvertor.Convert(price);
            return priceInfo;
        }

        private List<EventSchedule> scrapHallSessions(HtmlNode cinemaHall)
        {
            var hallSessions = new List<EventSchedule>();

            var dates = cinemaHall.CssSelect(".timewrap > .event").ToList();
            var bookableDates = cinemaHall.CssSelect(".timewrap > .activeEvent");

            dates.AddRange(bookableDates);

            foreach (var date in dates)
            {
                var sessionDate = scrapSessionDate(date);

                hallSessions.Add(new EventSchedule()
                {
                    Date = sessionDate,
                });
            }

            return hallSessions;
        }

        private DateTime scrapSessionDate(HtmlNode date)
        {
            var sessionDate = new DateTime();

            var dateText = date.InnerText;

            sessionDate = DateTime.ParseExact(dateText, "HH:mm",
                   System.Globalization.CultureInfo.InvariantCulture);

            return sessionDate;
        }

    }
}