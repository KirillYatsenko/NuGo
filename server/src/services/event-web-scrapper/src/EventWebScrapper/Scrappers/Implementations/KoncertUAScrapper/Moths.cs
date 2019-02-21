using System;
using System.Collections.Generic;

namespace EventWebScrapper.Scrappers
{
    public static class MonthsConvertor
    {
        private static readonly Dictionary<string, string> Months = new Dictionary<string, string>
        {
            {"января", "01"},
            {"февраля", "01"},
            {"марта", "01"},
            {"апреля", "01"},
            {"мая", "01"},
            {"июня", "01"},
            {"июля", "01"},
            {"августа", "01"},
            {"сентября", "01"},
            {"октебря", "01"},
            {"ноября", "01"},
            {"декабря", "01"},
        };

        public static string ReplaceMonth(string timeText)
        {
            var newTimeText = timeText;

            foreach (var month in Months)
            {
                if (timeText.Contains(month.Key))
                {
                    newTimeText = timeText.Replace(month.Key, month.Value);
                    return newTimeText;
                }
            }

            throw new ArgumentException("Month is not found");
        }

    }
}