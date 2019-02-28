using System;
using System.Linq;
using EventWebScrapper.Models;
using EventWebScrapper.Scrappers.KoncertUAScrappers;

namespace EventWebScrapper.Scrappers.KinoAfishaScrappers
{
    public static class KinoAfishaPriceConvertor
    {
        public static PriceInfo Convert(string price)
        {
            if (string.IsNullOrWhiteSpace(price))
            {
               return null;
            }

            var priceInfoResult = new PriceInfo();

            // 65..105
            var prices = price.Split("..").ToList();

            if (prices.Count == 1)
            {
                priceInfoResult = convertPrice(prices[0]);
            }
            else if (prices.Count == 2)
            {
                var minMaxPrice = new PriceInfo();
                var minPrice = convertPrice(prices[0]);
                var maxPrice = convertPrice(prices[1]);

                minMaxPrice.Currency = minPrice.Currency;
                minMaxPrice.Min = minPrice.Min;
                minMaxPrice.Max = maxPrice.Min;

                priceInfoResult = minMaxPrice;
            }

            return priceInfoResult;
        }

        private static PriceInfo convertPrice(string price)
        {
            return KoncertUaPriceConvertor.Convert(price);
        }

    }
}