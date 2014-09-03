using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using TVDBSharp.Models;
using TVDBSharp.Models;

namespace TVDBSharp.Services.Series
{
    public interface _ISeriesService
    {
        Task<Series> RetrieveFull(int showId, Language language);

        Task<Series> Retrieve(int showId, Language language);

        Task<IEnumerable<Series>> Search(string query, Language language);

        Series Parse(XElement element);
    }
}
