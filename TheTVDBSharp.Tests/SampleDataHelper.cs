using System;
using System.IO;

namespace TheTVDBSharp.Tests
{
    internal static class SampleDataHelper
    {
        internal enum SampleData
        {
            Episode306213,
            SearchScrubs,
            Series76156,
            SeriesFull76156,
            SeriesFull76156Meta,
            SeriesFull76156Banners,
            SeriesFull76156Actors,
            UpdatesDay
        }

        internal static string GetText(SampleData sampleData)
        {
            
            return System.IO.File.ReadAllText($"{AppContext.BaseDirectory}/SampleData/{sampleData.ToString()}.xml");
        }

        internal static Stream GetStream(SampleData sampleData)
        {
            return System.IO.File.OpenRead($"{AppContext.BaseDirectory}/SampleData/{sampleData.ToString()}.zip");
        }
    }
}
