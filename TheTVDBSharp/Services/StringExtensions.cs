using System;
using System.Collections.Generic;

namespace TheTVDBSharp.Services
{
    public static class StringExtensions
    {
        public static IReadOnlyCollection<string> SplitByPipe(this string raw)
        {
            if (String.IsNullOrWhiteSpace(raw)) return null;

            return raw.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
