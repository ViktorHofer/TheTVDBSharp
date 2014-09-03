using System.Collections.Generic;
using System.Xml.Linq;
using TVDBSharp.Models;

namespace TVDBSharp.Services
{
    public class ActorParseService : IActorParseService
    {
        public IReadOnlyCollection<Actor> Parse(string actorCollectionRaw)
        {
            var doc = XDocument.Parse(actorCollectionRaw);
            var actorsXml = doc.Element("Actors");

            var actorList = new List<Actor>();
            foreach (var actorXml in actorsXml.Elements("Actor"))
            {
                var actor = Parse(actorXml);
                actorList.Add(actor);
            }

            return actorList;
        }

        public Actor Parse(XElement actorXml)
        {
            var id = actorXml.ElementAsUInt("id");
            if (!id.HasValue) return null;

            return new Actor(id.Value)
            {
                ImageRemotePath = actorXml.ElementAsString("Image"),
                Name = actorXml.ElementAsString("Name"),
                Role = actorXml.ElementAsString("Role"),
                SortOrder = actorXml.ElementAsInt("SortOrder").Value
            };
        }
    }
}
