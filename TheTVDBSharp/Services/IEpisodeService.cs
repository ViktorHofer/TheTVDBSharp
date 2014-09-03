using System.Threading.Tasks;
using TheTVDBSharp.Models;

namespace TheTVDBSharp.Services
{
    public interface IEpisodeService
    {
        /// <summary>
        ///     Retrieves the episode with the given id and returns the corresponding XML tree.
        /// </summary>
        /// <param name="episodeId">ID of the episode to retrieve</param>
        /// <param name="lang">ISO 639-1 language code of the episode</param>
        Task<string> Retrieve(uint episodeId, Language language);
    }
}
