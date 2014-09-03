using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using TVDBSharp.Models;

namespace TVDBSharp.Services
{
    public interface ISeriesParseService
    {
        Series Parse(string seriesRaw);

        Series Parse(XElement seriesXml);

        Task<Series> ParseFull(Stream fullSeriesCompressedStream, Language language);

        IReadOnlyCollection<Series> ParseSearch(string seriesCollectionRaw);
    }
}
