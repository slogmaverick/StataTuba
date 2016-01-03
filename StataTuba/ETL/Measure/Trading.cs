using StataTuba.Common.Trading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StataTuba.ETL.Measure
{
    public static class Trading
    {
        /// <summary>
        /// Clears out all existing Simple Moving Averages and generates new SMAs.
        /// </summary>
        /// <param name="i"></param>
        public static void UpdateAllSMAs(Instrument i )
        {
            i.Measures.Values.ToList().ForEach(rd => rd.SMAs.Clear());
            TicTacTecaTuba.BuildSMA(i.Measures.Values.ToList(), 10);
            TicTacTecaTuba.BuildSMA(i.Measures.Values.ToList(), 14);
            TicTacTecaTuba.BuildSMA(i.Measures.Values.ToList(), 20);
            TicTacTecaTuba.BuildSMA(i.Measures.Values.ToList(), 50);
            TicTacTecaTuba.BuildSMA(i.Measures.Values.ToList(), 200);
        }
        /// <summary>
        /// Looks for 50 & 200 day moving average cross overs.  
        /// 50 goes below the 200 is a Death Cross.
        /// 50 goes above the 200 is a Golden Cross.
        /// </summary>
        public static void DoCrosses(Instrument i)
        {
            i.EODEvents.Clear();
            bool isInDeath = i.Measures.FirstOrDefault().Value.SMAs[50] < i.Measures.FirstOrDefault().Value.SMAs[200];
            foreach (var eod in i.Measures.Values)
            {
                if (eod.SMAs[50] < eod.SMAs[200] && !isInDeath)
                {
                    isInDeath = true;
                    i.EODEvents.Add(new EODEvent { CrossEventType = CrossEventTypeEnum.DeathCross, EODDate = eod.EODDate });
                }
                else if (eod.SMAs[50] > eod.SMAs[200] && isInDeath)
                {
                    isInDeath = false;
                    i.EODEvents.Add(new EODEvent { CrossEventType = CrossEventTypeEnum.GoldenCross, EODDate = eod.EODDate });
                }
            }
        }



    }
}
