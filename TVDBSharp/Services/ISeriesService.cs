using System.IO;
using System.Threading.Tasks;
using TVDBSharp.Models;

namespace TVDBSharp.Services
{
    public interface ISeriesService
    {
        /// <summary>
        ///     Retrieves the complete show with the given id and returns the xml string.
        /// </summary>
        /// <param name="showID">ID of the show you wish to lookup.</param>
        Task<Stream> RetrieveFull(int showId, Language language);

        /// <summary>
        ///     Retrieves the show with the given id and returns the xml string.
        /// </summary>
        /// <param name="showID">ID of the show you wish to lookup.</param>
        Task<string> Retrieve(int showId, Language language);

        /// <summary>
        ///     Returns the xml string representing a search response for the given parameter.
        /// </summary>
        /// <param name="query">Query to perform the search with. E.g. series title.</param>
        Task<string> Search(string query, Language language);
    }
}
