using StataTuba.Common.Trading;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StataTuba.Common.ExtensionMethod;

namespace StataTuba.ETL.Transform
{
    public static class Trading
    {
        /// <summary>
        /// Adjusts the OHLC prices of an instrument if a Split event is found.
        /// The original prices for Open and Close are stored in the OrigOpen and OrigClose properties.  These are used to calculate prices during back testing.
        /// </summary>
        public static void FindAndFixSplits(Instrument i)
        {
            var curClose = 0.0M;
            var splitDates = new Dictionary<DateTime, decimal>();
            foreach (var eq in i.Measures.Values.OrderBy(rd => rd.EODDate))
            {
                if (!eq.EODDate.IsTradingDay()) continue;
                if (curClose > 0.0M && eq.Open > 0.0M && curClose > 50.0M)
                {
                    var diff = curClose / eq.Open;
                    if (diff > 1.9M)
                    {
                        //We have a split!
                        var splitAmount = Math.Round(diff, 0);
                        Trace.WriteLine(i.Ticker + " " + splitAmount + " on " + eq.EODDate.PreviousTradingDay().ToShortDateString() + " open " + eq.Open);
                        splitDates.Add(eq.EODDate, (int)splitAmount);
                        i.EODEvents.Add(new EODEvent { CrossEventType = CrossEventTypeEnum.Split, EODDate = eq.EODDate, EventInt1 = (int)splitAmount });
                    }
                }
                curClose = eq.Close;
            }

            if (splitDates.Count > 0)
            {
                foreach (var splitDate in splitDates)
                {
                    i.Measures.Values.Where(rd => rd.EODDate.Date <= splitDate.Key.Date).ToList().ForEach(
                        delegate (InstrumentMeasure im)
                        {
                            im.OrigOpen = im.Open;
                            im.OrigClose = im.OrigClose;
                            im.Open = Math.Round(im.Open / splitDate.Value, 2);
                            im.High = Math.Round(im.High / splitDate.Value, 2);
                            im.Low = Math.Round(im.Low / splitDate.Value, 2);
                            im.Close = Math.Round(im.Close / splitDate.Value, 2);
                        }
                        );
                }
            }
        }  //End method

    }
}
