using System;

namespace TVDBSharp.Models
{
    public static class IntervalExtension
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
                    throw new ArgumentException("Unsupported interval enum: " + interval);
            }
        }
    }
}
