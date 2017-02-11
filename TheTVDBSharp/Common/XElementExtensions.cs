using System;
using System.Globalization;
using System.Xml.Linq;

namespace TheTVDBSharp.Common
{
    public static class XElementExtensions
    {
        public static uint? ElementAsUInt(this XElement parent, string element)
        {
            var value = parent.ElementAsString(element);
            if (value != null && uint.TryParse(value, out uint result))
            {
                return result;
            }

            return null;
        }

        public static bool? ElementAsBoolean(this XElement parent, string element)
        {
            var value = parent.ElementAsString(element);
            if (value != null && bool.TryParse(value, out bool result))
            {
                return result;
            }

            return null;
        }

        public static int? ElementAsInt(this XElement parent, string element)
        {
            var value = parent.ElementAsString(element);
            if (value != null && int.TryParse(value, out int result))
            {
                return result;
            }

            return null;
        }

        public static DateTime? ElementFromEpochToDateTime(this XElement parent, string element)
        {
            var epoch = ElementAsUInt(parent, element);
            if (epoch.HasValue)
            {
                return epoch.Value.ToDateTime();
            }

            return null;
        }

        public static DateTime? ElementAsDateTime(this XElement parent, string element)
        {
            var value = parent.ElementAsString(element);
            if (value != null && DateTime.TryParse(value, out DateTime result))
            {
                return result;
            }

            return null;
        }

        public static TimeSpan? ElementAsTimeSpan(this XElement parent, string element)
        {
            var value = parent.ElementAsString(element);
            if (value != null && DateTime.TryParse(value, out DateTime result))
            {
                return result.TimeOfDay;
            }

            return null;
        }

        public static TEnum? ElementAsEnum<TEnum>(this XElement parent, string element) where TEnum : struct
        {
            var value = ElementAsString(parent, element);
            if (value != null && Enum.TryParse(value, out TEnum result))
            {
                return result;
            }

            return null;
        }

        public static double? ElementAsDouble(this XElement parent, string element)
        {
            var value = parent.ElementAsString(element);
            if (value != null && double.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double result))
            {
                return result;
            }

            return null;
        }

        public static string ElementAsString(this XElement parent, string element, bool trim = false)
        {
            var child = parent.Element(element);
            return trim ? child?.Value.Trim() : child?.Value;
        }
    }
}
