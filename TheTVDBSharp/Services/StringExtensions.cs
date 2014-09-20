using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace TheTVDBSharp.Services
{
    public static class StringExtensions
    {
        public static XDocument ToXDocument(this string raw)
        {
            try
            {
                return XDocument.Parse(raw);
            }
            catch (XmlException)
            {
                return null;
            }
        }

        public static IReadOnlyCollection<string> SplitByPipe(this string raw)
        {
            if (String.IsNullOrWhiteSpace(raw)) return null;

            return raw.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
