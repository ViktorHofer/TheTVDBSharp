using System.IO;
using System.Xml.Linq;

namespace TVDBSharp.Testing.Component
{
    internal static class SampleDataHelper
    {
        internal enum SampleData
        {
            Episode_306213,
            Search_Scrubs,
            Series_76156,
            SeriesFull_76156,
            SeriesFull_76156_Meta,
            SeriesFull_76156_Banners,
            SeriesFull_76156_Actors
        }

        internal static string Open(SampleData sampleData)
        {
            return System.IO.File.ReadAllText("SampleData/" + sampleData.ToString() + ".xml");
        }

        internal static FileStream OpenStream(SampleData sampleData)
        {
            return System.IO.File.OpenRead("SampleData/" + sampleData.ToString() + ".zip");
        }
    }
}
