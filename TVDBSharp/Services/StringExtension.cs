using System;
using System.Collections.Generic;

namespace TVDBSharp.Services
{
    public static class StringExtension
    {
        public static IReadOnlyCollection<string> SplitByPipe(this string raw)
        {
            if (String.IsNullOrWhiteSpace(raw)) return null;

            return raw.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
