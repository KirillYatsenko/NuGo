using System;
using System.Text.RegularExpressions;
using EventWebScrapper.Models;

namespace EventWebScrapper.Scrappers.KoncertUAScrappers
{
    public static class KoncertUaPriceConvertor
    {
        public static PriceInfo Convert(string price)
        {
            if (string.IsNullOrWhiteSpace(price))
            {
                return null;
            }

            price = Regex.Match(price, @"\d+").Value;
            var parsedPrice = Decimal.Parse(price);

            var priceInfo = new PriceInfo()
            {
                Min = parsedPrice,  
                Currency = "грн."
            };

            return priceInfo;
        }

    }
}