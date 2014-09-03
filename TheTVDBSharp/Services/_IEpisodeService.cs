using System.IO;
using System.Threading.Tasks;
using TVDBSharp.Models;
using TVDBSharp.Models;

namespace TVDBSharp.Services.Episode
{
    public interface _IEpisodeService
    {
        Task<Episode> Retrieve(int episodeID, Language language);

        Episode Parse(Stream data);
    }
}
