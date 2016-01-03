using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StataTuba.ETL.Scraping
{
    public static class Misc
    {
        /// <summary>
        /// How much time in Milliseconds to sleep between scrapes.   This is the minimum amount.
        /// </summary>
        private const int scrapingSleepIntervalMin = 1000;
        /// <summary>
        /// How much time in Milliseconds to sleep between scrapes.   This is the Maximum amount.
        /// </summary>
        private const int scrapingSleepIntervalMax = 3000;

        public static string GetStuff(string url)
        {
            using (var wc = new WebClient())
            {
                var html = wc.DownloadString(url);
                wc.Dispose();  //Yeah, so.  Gotta a problem with this?
                var rnd = new Random();
                System.Threading.Thread.Sleep(rnd.Next(scrapingSleepIntervalMin, scrapingSleepIntervalMax));
                return html;
            }
        }
        public static void GetStuff(string url, string saveFilePath)
        {
            using (var wc = new WebClient())
            {
                wc.DownloadFile(url, saveFilePath);
                wc.Dispose();
                var rnd = new Random();
                System.Threading.Thread.Sleep(rnd.Next(scrapingSleepIntervalMin, scrapingSleepIntervalMax));
            }
        }
    }
}
