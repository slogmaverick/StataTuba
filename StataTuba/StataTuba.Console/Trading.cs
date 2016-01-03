using StataTuba.Common;
using StataTuba.Common.ExtensionMethod;
using StataTuba.Common.Trading;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StataTuba.Console
{
    public static class Trading
    {
        public static void StartHere()
        {
            CrossExamination();
        }

        /// <summary>
        /// Once this runs, you have the tab delimited data.  You'll have to add it to your spreadsheet and manually move stuff around.
        /// </summary>
        private static void CrossExamination()
        {
            //Purpose of this is to get some tickers.  Adjust for Splits.  Create moving averages.  Document Crosses.
            var amznFile = Path.Combine(Vars.Paths.YahooFinance, "AMZN.csv");
            var spyFile = Path.Combine(Vars.Paths.YahooFinance, "SPY.csv");
            if (!File.Exists(amznFile)) ETL.Scraping.Yahoo.GetTickerHistory("AMZN", amznFile);
            if (!File.Exists(spyFile)) ETL.Scraping.Yahoo.GetTickerHistory("SPY", spyFile);

            //Get the source data.
            var iSpy = InstrumentMeasure.FromYahoo("SPY");
            var iAmzn = InstrumentMeasure.FromYahoo("AMZN");

            //Update the measures
            ETL.Measure.Trading.UpdateAllSMAs(iAmzn);
            ETL.Measure.Trading.UpdateAllSMAs(iSpy);
            ETL.Measure.Trading.DoCrosses(iAmzn);
            ETL.Measure.Trading.DoCrosses(iSpy);
            ETL.Transform.Trading.FindAndFixSplits(iAmzn);

            var fmt = StringFormats.BuildDelimiter("\t", 5);
            var csv = string.Format(fmt,
                "Ticker",
                "Event",
                "Date",
                "Note",
                "AMZN Next Open"
                );

            //SPY
            foreach (var ev in iSpy.EODEvents.OrderBy(rd => rd.EODDate))
            {
                var nextDay = ev.EODDate.NextTradingDay();
                var eq = iAmzn.Measures.Values.Where(rd => rd.EODDate == nextDay).FirstOrDefault();
                csv += string.Format(fmt,
                    "SPY",
                    ev.CrossEventType,
                    ev.EODDate.ToShortDateString(),
                    ev.EventInt1,
                    Math.Round(eq.Open, 2)
                    );

                Trace.WriteLine(ev.EODDate.ToShortDateString() + " - " + ev.CrossEventType.ToString() + " - " + ev.EventInt1);
            }

            //AMZN
            foreach (var ev in iAmzn.EODEvents.OrderBy(rd => rd.EODDate))
            {
                var nextDay = ev.EODDate.NextTradingDay();
                var eq = iAmzn.Measures.Values.Where(rd => rd.EODDate == nextDay).FirstOrDefault();
                csv += string.Format(fmt,
                    "AMZN",
                    ev.CrossEventType,
                    ev.EODDate.ToShortDateString(),
                    ev.EventInt1,
                    Math.Round(eq.OrigOpen > 0.0M ? eq.OrigOpen : eq.Open, 2)
                    );

                Trace.WriteLine(ev.EODDate.ToShortDateString() + " - " + ev.CrossEventType.ToString() + " - " + ev.EventInt1);
            }

            Trace.WriteLine(csv);
        }


    }
}
