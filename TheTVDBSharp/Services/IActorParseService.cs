using System.Collections.Generic;
using System.Xml.Linq;
using TheTVDBSharp.Models;

namespace TheTVDBSharp.Services
{
    public interface IActorParseService
    {
        IReadOnlyCollection<Actor> Parse(string actorCollectionRaw);

        Actor Parse(XElement actorXml);
    }
}
