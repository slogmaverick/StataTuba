using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StataTuba.ETL.Scraping
{
    public static class Yahoo
    {
        /// <summary>
        /// Gets the EOD data from Yahoo for a given ticker symbol.  Saves to a file if a path is specified
        /// </summary>
        public static string GetTickerHistory(string ticker, string saveFilePath = "")
        {
            var minDate = new DateTime(1997, 1, 1);
            var maxDate = DateTime.Now;
            //http://real-chart.finance.yahoo.com/table.csv?s=AMZN&d=11&e=31&f=2015&g=d&a=4&b=16&c=1997&ignore=.csv

            var fmt = "http://real-chart.finance.yahoo.com/table.csv?s={0}&d={1}&e={2}&f={3}&g=d&a={4}&b={5}&c={6}&ignore=.csv";
            var url = string.Format(fmt,
                ticker,
                maxDate.Month,
                maxDate.Day,
                maxDate.Year,
                minDate.Month,
                minDate.Day,
                minDate.Year
                );
            var eod = "";

            if (string.IsNullOrEmpty(saveFilePath))
            {
                //Just get the EOD data as a string.
                eod = Misc.GetStuff(url);
            }
            else
            {
                //Just download and save to a file.
                Misc.GetStuff(url, saveFilePath);
            }

            return eod;
        }
    }
}
