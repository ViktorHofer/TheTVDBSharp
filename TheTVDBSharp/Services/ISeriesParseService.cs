using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using TheTVDBSharp.Models;

namespace TheTVDBSharp.Services
{
    public interface ISeriesParseService
    {
        Series Parse(string seriesRaw);

        Series Parse(XElement seriesXml, bool isSearchElement = false);

        Task<Series> ParseFull(Stream fullSeriesStream, Language language);

        IReadOnlyCollection<Series> ParseSearch(string seriesCollectionRaw);
    }
}
