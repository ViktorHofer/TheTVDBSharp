using System;

namespace TheTVDBSharp.Models
{
    public enum Interval
    {
        Day,
        Week,
        Month,
        All
    }

    public static class IntervalExtensions
    {
        public static string ToApiString(this Interval interval)
        {
            switch (interval)
            {
                case Interval.Day:
                    return "day";

                case Interval.Week:
                    return "week";

                case Interval.Month:
                    return "month";

                case Interval.All:
                    return "all";

                default:
                    throw new ArgumentOutOfRangeException("Unsupported interval enum: " + interval);
            }
        }
    }
}