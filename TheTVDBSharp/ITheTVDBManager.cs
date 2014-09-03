using System.Collections.Generic;
using System.Threading.Tasks;
using TheTVDBSharp.Models;

namespace TheTVDBSharp
{
    public interface ITheTVDBManager
    {
        Task<IReadOnlyCollection<Series>> SearchSeries(string query, Language language);

        Task<Series> GetSeries(uint showId, Language language);

        Task<Series> GetFullSeries(uint showId, Language language);

        Task<Episode> GetEpisode(uint episodeId, Language language);
    }
}
