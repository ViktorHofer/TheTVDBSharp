using System;
using System.Collections.Generic;

namespace TheTVDBSharp.Common
{
    public static class StringExtensions
    {
        public static IReadOnlyCollection<string> SplitByPipe(this string raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return null;

            return raw.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
