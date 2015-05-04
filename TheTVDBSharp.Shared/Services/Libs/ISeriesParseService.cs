using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using TheTVDBSharp.Models;

namespace TheTVDBSharp.Services.Libs
{
    public interface ISeriesParseService
    {
        Series Parse(string seriesRaw);

        Series Parse(XElement seriesXml, bool isSearchElement = false);

#if PORTABLE
        Task<Series> ParseFull(System.IO.Stream fullSeriesStream, Language language);
#elif WINDOWS_UAP
        Task<Series> ParseFull(Windows.Storage.Streams.IInputStream fullSeriesStream, Language language);
#endif

        IReadOnlyCollection<Series> ParseSearch(string seriesCollectionRaw);
    }
}
