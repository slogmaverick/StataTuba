using StataTuba.Common.Trading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StataTuba.ETL.Measure
{
    public static class TicTacTecaTuba
    {
        /// <summary>
        /// Generates Simple Moving Averages based on the Adjusted Close (just Close property).
        /// If the number of periods is less than the measure period a value of 0.0 is used.
        /// </summary>
        internal static void BuildSMA(List<InstrumentMeasure> ims, int period)
        {
            foreach (var im in ims.OrderBy(rd => rd.EODDate))
            {
                var periodPrices = ims.Where(rd => rd.EODDate <= im.EODDate).OrderByDescending(rd => rd.EODDate).Take(period).ToList();
                var avg = 0.0M;
                if (periodPrices.Count == period) avg = periodPrices.Select(rd => rd.Close).Average();
                im.SMAs.Add(period, Math.Round(avg, 2));
            }
        }

    }
}
