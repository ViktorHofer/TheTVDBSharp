using System.IO;

namespace TheTVDBSharp.Testing
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

        internal static string Open(SampleData sampleData)
        {
            return File.ReadAllText("SampleData/" + sampleData.ToString() + ".xml");
        }

        internal static FileStream OpenStream(SampleData sampleData)
        {
            return File.OpenRead("SampleData/" + sampleData.ToString() + ".zip");
        }
    }
}
