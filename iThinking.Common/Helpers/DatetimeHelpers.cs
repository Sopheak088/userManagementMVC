using System;
using System.Globalization;

namespace iThinking.Common.Helpers
{
    public static class DatetimeHelpers
    {
        public static double GetBusinessDays(DateTime startDate, DateTime endDate)
        {
            double calcBusinessDays =
                1 + ((endDate.Date - startDate.Date).TotalDays * 5 -
                (startDate.DayOfWeek - endDate.DayOfWeek) * 2) / 7;

            if (endDate.DayOfWeek == DayOfWeek.Saturday) calcBusinessDays--;
            if (startDate.DayOfWeek == DayOfWeek.Sunday) calcBusinessDays--;

            return calcBusinessDays;
        }

        public static int DistanceDateTime(DateTime startDate, DateTime endDate)
        {
            int numberdate = endDate.Date.Subtract(startDate.Date).Days;

            if (numberdate >= 0)
                return numberdate + 1;
            else
                return numberdate;
        }

        public static int CompareDateTime(DateTime startDate, DateTime endDate)
        {
            int numberdate = endDate.Date.CompareTo(startDate.Date);

            return numberdate;
        }

        public static DateTime FirstDayOfWeek(DateTime date)
        {
            DayOfWeek fdow = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            int offset = fdow - date.DayOfWeek;
            DateTime fdowDate = date.AddDays(offset);
            return fdowDate;
        }

        public static DateTime LastDayOfWeek(DateTime date)
        {
            DateTime ldowDate = FirstDayOfWeek(date).AddDays(7);
            return ldowDate;
        }

        public static DateTime FirstDayOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        public static DateTime LastDayOfMonth(this DateTime dt)
        {
            return dt.FirstDayOfMonth().AddMonths(1).AddDays(-1);
        }

        public static DateTime FirstDayOfNextMonth(this DateTime dt)
        {
            return dt.FirstDayOfMonth().AddMonths(1);
        }
    }
}