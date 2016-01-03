using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StataTuba.Common.ExtensionMethod
{
    public static class DateTimeExt
    {

        public static DateTime NextTradingDay(this DateTime dt)
        {
            var targetDt = dt;
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    targetDt = dt.AddDays(3);
                    break;
                case DayOfWeek.Saturday:
                    targetDt = dt.AddDays(2);
                    break;
                default:
                    targetDt = dt.AddDays(1);
                    break;
            }
            while (!targetDt.IsTradingDay())
            {
                targetDt = targetDt.AddDays(1);
            }
            return targetDt;
        }
        public static DateTime NextTradingDay(this DateTime dt, int daysForward)
        {
            var targetDt = dt;
            var daysGoneForward = 0;
            while (daysGoneForward < daysForward)
            {
                targetDt = targetDt.AddDays(1);
                if (targetDt.IsTradingDay()) daysGoneForward++;
            }
            return targetDt;
        }
        public static DateTime PreviousTradingDay(this DateTime dt)
        {
            var targetDt = dt.AddDays(-1);
            while (!targetDt.IsTradingDay())
            {
                targetDt = targetDt.AddDays(-1);
            }
            return targetDt;
        }
        public static DateTime PreviousTradingDay(this DateTime dt, int daysBack)
        {
            var daysGoneBack = 0;
            var targetDt = dt;
            while (daysGoneBack < daysBack)
            {
                targetDt = targetDt.AddDays(-1);
                if (targetDt.IsTradingDay()) daysGoneBack++;
            }
            return targetDt;
        }
        public static DateTime GetLastTradingDay(this DateTime dt)
        {
            if (dt.Hour < 15) dt = dt.AddDays(-1);
            while (!dt.IsTradingDay())
            {
                dt = dt.AddDays(-1);
            }
            return dt.Date;
        }
        public static bool IsTradingDay(this DateTime dt)
        {
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                    return false;
                default:
                    return !IsTradingHoliday(dt);
            }
        }
        public static bool IsTradingHoliday(this DateTime dt)
        {
            if (dt.Month == 1 && dt.Day == 1) return true;
            if (dt.Month == 1 && dt.Day == 2 && dt.DayOfWeek == DayOfWeek.Monday) return true;
            if (dt.Month == 12 && dt.Day == 25) return true;
            if (dt.Month == 7 && dt.Day == 4) return true;
            if (dt.Month == 5)
            {
                //Memorial Day 
                var lastMonday = dt.EndOfMonth();
                while (lastMonday.DayOfWeek != DayOfWeek.Monday)
                {
                    lastMonday = lastMonday.AddDays(-1);
                }
                if (dt.Date == lastMonday.Date) return true;
            }
            if (dt.Month == 9)
            {
                //Labor Day 
                var firstMonday = dt.BeginningOfMonth();
                while (firstMonday.DayOfWeek != DayOfWeek.Monday)
                {
                    firstMonday = firstMonday.AddDays(1);
                }
                if (dt.Date == firstMonday.Date) return true;
            }
            if (dt.Month == 1)
            {
                //MLK Day
                var mlkMonday = dt.BeginningOfMonth();
                var mondayCount = 0;
                while (1 == 1)
                {
                    if (mlkMonday.DayOfWeek == DayOfWeek.Monday) mondayCount++;
                    if (mondayCount == 3) break;
                    mlkMonday = mlkMonday.AddDays(1);
                }
                if (dt.Date == mlkMonday.Date) return true;
            }
            if (dt.Month == 2)
            {
                //President's Day
                var presidentsMonday = dt.BeginningOfMonth();
                var mondayCount = 0;
                while (1 == 1)
                {
                    if (presidentsMonday.DayOfWeek == DayOfWeek.Monday) mondayCount++;
                    if (mondayCount == 3) break;
                    presidentsMonday = presidentsMonday.AddDays(1);
                }
                if (dt.Date == presidentsMonday.Date) return true;
            }
            if (dt.Month == 11)
            {
                //Thanksgiving Day
                var thanksgiving = dt.BeginningOfMonth();
                var thursdayCount = 0;
                while (1 == 1)
                {
                    if (thanksgiving.DayOfWeek == DayOfWeek.Thursday) thursdayCount++;
                    if (thursdayCount == 4) break;
                    thanksgiving = thanksgiving.AddDays(1);
                }
                if (dt.Date == thanksgiving.Date) return true;
            }

            //Good Friday
            if (dt.Date.DayOfWeek == DayOfWeek.Friday)
            {
                if (dt.Date == new DateTime(1990, 4, 13)) return true;
                if (dt.Date == new DateTime(1991, 3, 29)) return true;
                if (dt.Date == new DateTime(1992, 4, 17)) return true;
                if (dt.Date == new DateTime(1993, 4, 9)) return true;
                if (dt.Date == new DateTime(1994, 4, 1)) return true;
                if (dt.Date == new DateTime(1995, 4, 14)) return true;
                if (dt.Date == new DateTime(1996, 4, 5)) return true;
                if (dt.Date == new DateTime(1997, 3, 28)) return true;
                if (dt.Date == new DateTime(1998, 4, 10)) return true;
                if (dt.Date == new DateTime(1999, 4, 2)) return true;
                if (dt.Date == new DateTime(2000, 4, 21)) return true;
                if (dt.Date == new DateTime(2001, 4, 13)) return true;
                if (dt.Date == new DateTime(2002, 3, 29)) return true;
                if (dt.Date == new DateTime(2003, 4, 18)) return true;
                if (dt.Date == new DateTime(2004, 4, 9)) return true;
                if (dt.Date == new DateTime(2005, 3, 25)) return true;
                if (dt.Date == new DateTime(2006, 4, 14)) return true;
                if (dt.Date == new DateTime(2007, 4, 6)) return true;
                if (dt.Date == new DateTime(2008, 3, 21)) return true;
                if (dt.Date == new DateTime(2009, 4, 10)) return true;
                if (dt.Date == new DateTime(2010, 4, 2)) return true;
                if (dt.Date == new DateTime(2011, 4, 22)) return true;
                if (dt.Date == new DateTime(2012, 4, 6)) return true;
                if (dt.Date == new DateTime(2013, 3, 29)) return true;
                if (dt.Date == new DateTime(2014, 4, 18)) return true;
                if (dt.Date == new DateTime(2015, 4, 3)) return true;
                if (dt.Date == new DateTime(2016, 3, 25)) return true;
                if (dt.Date == new DateTime(2017, 4, 14)) return true;
                if (dt.Date == new DateTime(2018, 3, 30)) return true;
                if (dt.Date == new DateTime(2019, 4, 19)) return true;
                if (dt.Date == new DateTime(2020, 4, 10)) return true;
            }

            //Christmas Observed
            if (dt.Month == 12 && dt.Day == 24 && dt.DayOfWeek == DayOfWeek.Friday) return true;
            if (dt.Month == 12 && dt.Day == 26 && dt.DayOfWeek == DayOfWeek.Monday) return true;

            //July 4th Observed
            if (dt.Month == 7 && dt.Day == 3 && dt.DayOfWeek == DayOfWeek.Friday) return true;
            if (dt.Month == 7 && dt.Day == 5 && dt.DayOfWeek == DayOfWeek.Monday) return true;






            return false;
        }
        public static DateTime EndOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));
        }
        public static bool IsEndOfMonth(this DateTime dt)
        {
            var targetDate = dt.EndOfMonth();
            while (!targetDate.IsTradingDay())
            {
                targetDate = targetDate.AddDays(-1);
            }
            return dt.Date == targetDate.Date;
        }
        public static DateTime LastTradingDayOfMonth(this DateTime dt)
        {
            var targetDate = dt.EndOfMonth();
            while (!targetDate.IsTradingDay())
            {
                targetDate = targetDate.AddDays(-1);
            }
            return targetDate;
        }
        public static DateTime BeginningOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }
        public static int DaysInMonth(this DateTime dt)
        {
            return DateTime.DaysInMonth(dt.Year, dt.Month);
        }
        public static bool IsXTradingDayFromEndOfMonth(this DateTime dt, int x)
        {
            if (!dt.IsTradingDay()) throw new ArgumentException("Date must be a trading date.");
            var eom = dt.EndOfMonth();
            while (!eom.IsTradingDay())
            {
                eom = eom.AddDays(-1);
            }

            var tradingDaysCount = 0;
            for (int z = 0; z < dt.DaysInMonth(); z++)
            {
                if (dt.AddDays(z).IsTradingDay())
                {
                    if (dt.AddDays(z).Date == eom.Date && tradingDaysCount == x) return true;
                    tradingDaysCount++;
                    if (tradingDaysCount > x) return false;
                }
            }
            return false;
        }
        public static bool IsXTradingDayFromBeginningOfMonth(this DateTime dt, int x)
        {
            if (!dt.IsTradingDay()) throw new ArgumentException("Date must be a trading date.");
            var bom = dt.BeginningOfMonth();
            while (!bom.IsTradingDay())
            {
                bom = bom.AddDays(1);
            }

            var tradingDaysCount = 0;
            for (int z = 0; z < dt.DaysInMonth(); z++)
            {
                if (bom.AddDays(z).IsTradingDay())
                {
                    if (bom.AddDays(z).Date == dt.Date && tradingDaysCount == x) return true;
                    tradingDaysCount++;
                    if (tradingDaysCount > x) return false;
                }
            }
            return false;

        }
    }
}
