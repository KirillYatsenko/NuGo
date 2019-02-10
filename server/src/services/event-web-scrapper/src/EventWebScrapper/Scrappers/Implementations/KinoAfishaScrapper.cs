using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventWebScrapper.Enums;
using EventWebScrapper.Models;
using Microsoft.Extensions.Configuration;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace EventWebScrapper.Scrappers
{
    public class KinoAfishaScrapper : IKinoAfishaScrapper
    {
        private readonly string _kinoAfishaUri;

        public KinoAfishaScrapper(IConfiguration configuration)
        {
            _kinoAfishaUri = configuration["KinoAfishaUri"];

            if (string.IsNullOrWhiteSpace(_kinoAfishaUri))
            {
                throw new ConfigurationErrorsException("KinoAfishaUri entry can not be found, please check appsettings");
            }
        }

        public async Task<IEnumerable<Event>> Scrap()
        {
            var scrappedData = new List<Event>();

            ScrapingBrowser Browser = new ScrapingBrowser();
            Browser.AllowAutoRedirect = true;
            Browser.AllowMetaRedirect = true;
            Browser.Encoding = Encoding.UTF8;

            WebPage pageResult = await Browser.NavigateToPageAsync(new Uri($"{_kinoAfishaUri}/kinoafisha"));
            var filmsHtml = pageResult.Html.CssSelect(".list-films").First();
            var titlesHtml = filmsHtml.CssSelect(".item > .text > h3");

            foreach (var titleHtml in titlesHtml)
            {
                var relativeUrl = titleHtml.CssSelect("a")?.First().Attributes["href"].Value;
                var fullUrl = $"{_kinoAfishaUri}{relativeUrl}";

                var detailsPage = await Browser.NavigateToPageAsync(new Uri(fullUrl));

                var ratingString = detailsPage.Html.CssSelect("span[itemprop=ratingValue]").FirstOrDefault().InnerText;
                var ratingNumber = Decimal.Parse(ratingString.Replace(',','.'));

                var description = detailsPage.Html.CssSelect(".description").FirstOrDefault()?.InnerText;
                var relativePhotoUrl = detailsPage.Html.CssSelect(".photo").FirstOrDefault()?.Attributes["href"].Value;
                var fullImageUrl = $"{_kinoAfishaUri}{relativePhotoUrl}";

                List<EventDate> eventDates = scrapSessions(detailsPage);

                var newEvent = new Event()
                {
                    Title = titleHtml.InnerText,
                    Description = description,
                    DetailsUrl = fullUrl,
                    ImageUrl = fullImageUrl,
                    Dates = eventDates,
                    CategoryId = (int)EventCategories.Cinema,
                    Rating = ratingNumber
                };

                scrappedData.Add(newEvent);
            }

            return scrappedData;
        }

        private List<EventDate> scrapSessions(WebPage detailsPage)
        {
            var eventDates = new List<EventDate>();
            var filmSessions = detailsPage.Html.CssSelect(".film-sessions");
            var monthDay = detailsPage.Html.CssSelect(".today").FirstOrDefault()?.InnerText;

            foreach (var session in filmSessions)
            {
                var cinemaInfoRow = session.CssSelect("tr").FirstOrDefault();
                var address = cinemaInfoRow.CssSelect(".cinema-room > a").FirstOrDefault().Attributes["title"].Value;
                var cinemaName = cinemaInfoRow.CssSelect(".cinema-room > a > b").FirstOrDefault().InnerText;

                var cinemaRoomRows = session.CssSelect("tr").Skip(1);

                foreach (var cinemaRoomRow in cinemaRoomRows)
                {
                    var cinemaRoomPrice = cinemaRoomRow.CssSelect(".dotted > .note").FirstOrDefault()?.InnerText;

                    var dates = cinemaRoomRow.CssSelect(".timewrap > .event").ToList();
                    var bookableDates = cinemaRoomRow.CssSelect(".timewrap > .activeEvent");

                    dates.AddRange(bookableDates);

                    foreach (var date in dates)
                    {
                        var dateText = date.InnerText;

                        var parsedDate = DateTime.ParseExact(dateText, "HH:mm",
                                System.Globalization.CultureInfo.InvariantCulture);

                        eventDates.Add(new EventDate()
                        {
                            Date = parsedDate,
                            HostName = cinemaName,
                            Address = address,
                            Price = cinemaRoomPrice
                        });
                    }
                }

            }

            return eventDates;
        }


    }
}