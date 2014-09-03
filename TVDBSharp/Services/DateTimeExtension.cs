using System;
using System.Globalization;

namespace TVDBSharp.Services
{
    /// <summary>
    ///     Provides Date and Time extension methods.
    /// </summary>
    public static class DateTimeExtension
    {
        public static DateTime ToDateTime(this uint unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime).ToLocalTime();
        }

        /// <summary>
        ///     Parses a string of format hh:mm tt to a <see cref="TimeSpan" /> object.
        /// </summary>
        /// <param name="value">String to be parsed.</param>
        /// <returns>Returns a <see cref="TimeSpan" /> representation.</returns>
        public static TimeSpan? ToTimeSpan(this string value)
        {
            DateTime date;
            if (DateTime.TryParse(value, out date))
            {
                return date.TimeOfDay;
            }
            return null;
        }
    }
}