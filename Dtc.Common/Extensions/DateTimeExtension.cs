using System;

namespace Dtc.Common.Extensions
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// Duration from DateTime value
        /// </summary>
        /// <returns>TimeSpan duration</returns>
        public static TimeSpan ToDurationFrom(this DateTime value, DateTime? finish)
        {
            return (finish.HasValue && (value <= finish.Value)) ? (finish.Value - value) : new TimeSpan();
        }

        /// <summary>
        /// Time interval from Datetime.Now
        /// </summary>
        /// <param name="value">Datetime value</param>
        /// <returns>TimeSpan</returns>
        public static TimeSpan ToDurationFromNow(this DateTime value)
        {
            return value.ToDurationFrom(DateTime.Now);
        }

        public static bool DayIsSame(this DateTime value, DateTime dateTime)
        {
            return value.DateAsNumber() == dateTime.DateAsNumber();
        }

        public static bool DayIsSmallerThan(this DateTime value, DateTime dateTime)
        {
            return value.DateAsNumber() < dateTime.DateAsNumber();
        }

        public static bool DayIsBiggerThan(this DateTime value, DateTime dateTime)
        {
            return value.DateAsNumber() > dateTime.DateAsNumber();
        }

        public static int DateAsNumber(this DateTime value)
        {
            return (value.Year * 10000) + (value.Month * 100) + value.Day;
        }

        public static bool IsWeekend(this DateTime value)
        {
            return ((value.DayOfWeek == DayOfWeek.Saturday) || (value.DayOfWeek == DayOfWeek.Sunday));
        }

        public static DateTime DateOnly(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day);
        }

        public static DateTime GetPrevDayOfWeek(this DateTime value, DayOfWeek dayOfWeek) => value.GetDayOfWeekShift(dayOfWeek, -1);

        public static DateTime GetNextDayOfWeek(this DateTime value, DayOfWeek dayOfWeek) => value.GetDayOfWeekShift(dayOfWeek, 1);

        private static DateTime GetDayOfWeekShift(this DateTime value, DayOfWeek dayOfWeek, int direction)
        {
            var result = new DateTime(value.Year, value.Month, value.Day);
            // dolezu az na predchozi/nasledujici den dle typu
            while (result.DayOfWeek != dayOfWeek)
            {
                result = result.AddDays(direction);
            }
            return result;
        }
    }
}