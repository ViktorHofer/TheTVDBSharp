using System.IO;
using System.Threading.Tasks;
using TheTVDBSharp.Models;

namespace TheTVDBSharp.Services
{
    public interface ISeriesService
    {
        /// <summary>
        ///     Retrieves the complete show with the given id and returns the xml string.
        /// </summary>
        /// <param name="showId">Id of the show you wish to lookup.</param>
        /// <param name="language">ISO 639-1 language code of the episode</param>
        Task<Stream> RetrieveFull(uint showId, Language language);

        /// <summary>
        ///     Retrieves the show with the given id and returns the xml string.
        /// </summary>
        /// <param name="showId">Id of the show you wish to lookup.</param>
        /// <param name="language">ISO 639-1 language code of the episode</param>
        Task<string> Retrieve(uint showId, Language language);

        /// <summary>
        ///     Returns the xml string representing a search response for the given parameter.
        /// </summary>
        /// <param name="query">Query to perform the search with. E.g. series title.</param>
        /// <param name="language">ISO 639-1 language code of the episode</param>
        Task<string> Search(string query, Language language);
    }
}
