
using System.Collections.Generic;
using System.Xml.Linq;
using TVDBSharp.Models;

namespace TVDBSharp.Services
{
    public interface IActorParseService
    {
        IReadOnlyCollection<Actor> Parse(string actorCollectionRaw);

        Actor Parse(XElement actorXml);
    }
}
