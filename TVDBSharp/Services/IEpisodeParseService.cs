
using System.Xml.Linq;
using TVDBSharp.Models;

namespace TVDBSharp.Services
{
    public interface IEpisodeParseService
    {
        Episode Parse(string episodeRaw);

        Episode Parse(XElement episodeXml);
    }
}
