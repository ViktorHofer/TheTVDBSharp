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
        
        Task<Series> ParseFull(System.IO.Stream fullSeriesStream, Language language);

        IReadOnlyCollection<Series> ParseSearch(string seriesCollectionRaw);
    }
}
