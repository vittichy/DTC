using System;

namespace Dtc.Common.Extensions
{
    public static class TimeSpanExtension
    {
        /// <summary>
        /// Convert TimeSpan into ETA string like "1.22:33"
        /// </summary>
        public static string ToDuration(this TimeSpan value)
        {
            string format;
            if (value.TotalDays > 1)
            {
                format = "{0:d\\.hh\\:mm}";
            }
            else if (value.TotalHours > 1)
            {
                // TODO hodiny + minuty a minuty + sekundy se po naformatovani nerozpoznaji? jak rozlisit?
                format = "{0:hh\\:mm}";
            }
            else if (value.TotalMinutes > 1)
            {
                // TODO hodiny + minuty a minuty + sekundy se po naformatovani nerozpoznaji? jak rozlisit?
                format = "{0:mm\\:ss}";
            }
            else
            {
                format = "{0:ss\\.fff}";
            }
            var result = string.Format(format, value);
            return result;
        }
    }
}
