using System.Collections.Generic;
using System.Threading.Tasks;
using TVDBSharp.Models;

namespace TVDBSharp
{
    public interface ITVDBManager
    {
        Task<IReadOnlyCollection<Series>> SearchSeries(string query, Language language);

        Task<Series> GetSeries(int showId, Language language);

        Task<Series> GetFullSeries(int showId, Language language);

        Task<Episode> GetEpisode(int episodeId, Language language);
    }
}
