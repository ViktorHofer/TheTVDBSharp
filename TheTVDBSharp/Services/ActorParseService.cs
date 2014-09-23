using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using TheTVDBSharp.Models;

namespace TheTVDBSharp.Services
{
    public class ActorParseService : IActorParseService
    {
        private readonly ISimpleLogger logger;

        public ActorParseService(ISimpleLogger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Parse and actors collection as string and returns null if xml not valid
        /// </summary>
        /// <param name="actorCollectionRaw">Actors xml document</param>
        /// <returns>Returns the parsed actors collection or null if xml is not valid</returns>
        public IReadOnlyCollection<Actor> Parse(string actorCollectionRaw)
        {
            if (string.IsNullOrWhiteSpace(actorCollectionRaw)) throw new ArgumentNullException("actorCollectionRaw", "Actor collection xml document as string cannot be null");

            // If xml cannot be created return null
            XDocument doc;
            try
            {
                doc = XDocument.Parse(actorCollectionRaw);
            }
            catch (XmlException e)
            {
                this.logger.Log("Actors collection string cannot be parsed into a xml document.", LogLevel.Error, e);
                return null;
            }

            // If Actors element is missing return null
            var actorsXml = doc.Element("Actors");
            if (actorsXml == null)
            {
                this.logger.Log("Error while parsing actors xml document. Xml Element 'Actors' is missing.", LogLevel.Error);
                return null;
            }

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
            if (!id.HasValue)
            {
                this.logger.Log("Error while parsing an actor xml element. Id is missing.", LogLevel.Error);
                return null;
            }

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
