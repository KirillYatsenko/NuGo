using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<EventScrapData>> Scrap()
        {
            var scrappedData = new List<EventScrapData>();

            ScrapingBrowser Browser = new ScrapingBrowser();
            Browser.AllowAutoRedirect = true;
            Browser.AllowMetaRedirect = true;
            Browser.Encoding = Encoding.UTF8;

            WebPage pageResult = await Browser.NavigateToPageAsync(new Uri($"{_kinoAfishaUri}/kinoafisha"));
            var filmsHtml = pageResult.Html.CssSelect(".list-films").First();
            var titlesHtml = filmsHtml.CssSelect(".item > .text > h3");

            foreach (var titleHtml in titlesHtml)
            {
                var relativeUrl = titleHtml.CssSelect("a").First().Attributes["href"].Value;
                var fullUrl = $"{_kinoAfishaUri}{relativeUrl}";

                var detailsPage = await Browser.NavigateToPageAsync(new Uri(fullUrl));
                var description = detailsPage.Html.CssSelect(".description").First().InnerText;

                scrappedData.Add(new EventScrapData()
                {
                    Title = titleHtml.InnerText,
                    Description = description,
                    DetailsUrl = fullUrl,
                });

            }

            return null;
        }
    }
}