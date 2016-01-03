using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StataTuba.Common.Trading
{

    public enum CrossEventTypeEnum
    {
        GoldenCross = 1,
        DeathCross = 2,
        Split = 3
    }
    public class EODEvent
    {
        public DateTime EODDate { get; set; }
        public CrossEventTypeEnum CrossEventType { get; set; }
        public int EventInt1 { get; set; }
    }


    public class Instrument
    {
        public string Ticker { get; set; }
        public string Name { get; set; }

        public Dictionary<DateTime, InstrumentMeasure> Measures { get; set; }

        public List<EODEvent> EODEvents { get; set; }

        public Instrument()
        {
            Measures = new Dictionary<DateTime, InstrumentMeasure>();
            EODEvents = new List<EODEvent>();
        }
    }


   

    public class InstrumentMeasure
    {
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal OrigOpen { get; set; }
        public decimal OrigClose { get; set; }

        public long Volume { get; set; }
        public DateTime EODDate { get; set; }
        public Dictionary<int, decimal> SMAs { get; set; }

        public InstrumentMeasure()
        {
            SMAs = new Dictionary<int, decimal>();
        }


        public static Instrument FromYahoo(string ticker)
        {
            var filePath = Path.Combine(Vars.Paths.YahooFinance, ticker + ".csv");
            if (!File.Exists(filePath)) throw new FileNotFoundException("Try again dummy.  File: " + filePath);

            //Create new instrument to return;
            var i = new Instrument();
            i.Ticker = ticker;

            //Iterate through the lines to get the OHLCs
            var lines = File.ReadAllLines(filePath);
            var ims = new List<InstrumentMeasure>();
            foreach (var line in lines.Skip(1))
            {
                ims.Add(FromStandardFormat(line));
            }

            ims.OrderBy(rd => rd.EODDate).ToList().ForEach(rd => i.Measures.Add(rd.EODDate.Date, rd));

            return i;
        }

        private static InstrumentMeasure FromStandardFormat(string line)
        {
            var fields = line.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            //Early exit reasons.
            if (fields.Count() < 6) throw new ArgumentException("Invalid OHLC line.  It should have at least 6 fields dummy.  Here's what you gave me: " + line);

            var im = new InstrumentMeasure
            {
                EODDate = DateTime.Parse(fields[0]),
                Open = decimal.Parse(fields[1]),
                High = decimal.Parse(fields[2]),
                Low = decimal.Parse(fields[3]),
                Close = decimal.Parse(fields[4]),
                Volume = long.Parse(fields[5])

                //Yahoo has another field called Adjusted Close.  This field is the closing price adjusted for splits and dividends
                // I don't want it, it just confuses things.
            };
            return im;
        }

    }
}
