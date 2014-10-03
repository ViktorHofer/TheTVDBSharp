using System.Xml.Linq;
using TheTVDBSharp.Models;

namespace TheTVDBSharp.Services
{
    public interface IEpisodeParseService
    {
        Episode Parse(string episodeRaw);

        Episode Parse(XElement episodeXml);
    }
}
