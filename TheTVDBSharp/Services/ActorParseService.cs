using System;
using System.Collections.Generic;
using System.Xml.Linq;
using TheTVDBSharp.Models;

namespace TheTVDBSharp.Services
{
    public class ActorParseService : IActorParseService
    {
        /// <summary>
        /// Parse and actors collection as string and returns null if xml not valid
        /// </summary>
        /// <param name="actorCollectionRaw">Actors xml document</param>
        /// <returns>Returns the parsed actors collection or null if xml is not valid</returns>
        public IReadOnlyCollection<Actor> Parse(string actorCollectionRaw)
        {
            // If xml cannot be created return null
            var doc = actorCollectionRaw.ToXDocument();
            if (doc == null) return null;

            // If Actors element is missing return null
            var actorsXml = doc.Element("Actors");
            if (actorsXml == null) return null;

            var actorList = new List<Actor>();
            foreach (var actorXml in actorsXml.Elements("Actor"))
            {
                var actor = Parse(actorXml);
                if (actor != null) actorList.Add(actor);
            }

            return actorList;
        }

        /// <summary>
        /// Parse an actor xml element and returns null if xml not valid
        /// </summary>
        /// <param name="actorXml">Actor xml element</param>
        /// <returns>Returns parsed actor or null if xml is not valid</returns>
        public Actor Parse(XElement actorXml)
        {
            if (actorXml == null)
            {
                throw new ArgumentNullException("actorXml", "Actor xml cannot be null");
            }

            // If actors has no id skip parsing and return null
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
