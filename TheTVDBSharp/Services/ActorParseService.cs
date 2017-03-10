using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using TheTVDBSharp.Common;
using TheTVDBSharp.Models;
using TheTVDBSharp.Services.Libs;

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
            if (actorCollectionRaw == null) throw new ArgumentNullException("actorCollectionRaw");

            // If xml cannot be created return null
            XDocument doc;
            try
            {
                doc = XDocument.Parse(actorCollectionRaw);
            }
            catch (XmlException e)
            {
                throw new ParseException("Actors collection string cannot be parsed into a xml document.", e);
            }

            return doc.Element("Actors")?.Elements("Actor")
                .Select(Parse)
                .Where(actor => actor != null)
                .ToList() ?? throw new ParseException("Error while parsing actors xml document. Xml Element 'Actors' is missing.");
        }

        /// <summary>
        /// Parse an actor xml element and returns null if xml not valid
        /// </summary>
        /// <param name="actorXml">Actor xml element</param>
        /// <returns>Returns parsed actor or null if xml is not valid</returns>
        public Actor Parse(XElement actorXml)
        {
            if (actorXml == null) throw new ArgumentNullException(nameof(actorXml));

            // If actor has no id throw ParseException
            var id = actorXml.ElementAsUInt("id");
            if (!id.HasValue) throw new ParseException("Error while parsing an actor xml element. Id is missing.");

            return new Actor(id.Value)
            {
                ImageRemotePath = actorXml.ElementAsString("Image"),
                Name = actorXml.ElementAsString("Name"),
                Role = actorXml.ElementAsString("Role"),
                SortOrder = actorXml.ElementAsInt("SortOrder").GetValueOrDefault()
            };
        }
    }
}
