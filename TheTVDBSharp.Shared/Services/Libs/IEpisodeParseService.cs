using System.Xml.Linq;
using TheTVDBSharp.Models;

namespace TheTVDBSharp.Services.Libs
{
    public interface IEpisodeParseService
    {
        Episode Parse(string episodeRaw);

        Episode Parse(XElement episodeXml);
    }
}
