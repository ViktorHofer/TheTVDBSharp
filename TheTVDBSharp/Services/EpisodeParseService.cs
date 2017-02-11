using System;
using System.Xml;
using System.Xml.Linq;
using TheTVDBSharp.Common;
using TheTVDBSharp.Models;
using TheTVDBSharp.Services.Libs;

namespace TheTVDBSharp.Services
{
    public class EpisodeParseService : IEpisodeParseService
    {
        /// <summary>
        /// Parse episode xml document as string and return null if xml is not valid
        /// </summary>
        /// <param name="episodeRaw">Episode xml document as string</param>
        /// <returns>Return parsed episode or null if xml is not valid</returns>
        public Episode Parse(string episodeRaw)
        {
            if (episodeRaw == null) throw new ArgumentNullException(nameof(episodeRaw));

            // If xml cannot be created return null
            XDocument doc;
            try
            {
                doc = XDocument.Parse(episodeRaw);
            }
            catch (XmlException e)
            {
                throw new ParseException("Episode string cannot be parsed into a xml document.", e);
            }

            // If Data element is missing return null
            var dataXml = doc.Element("Data");
            if (dataXml == null) throw new ParseException("Error while parsing episode xml document. Xml Element 'Data' is missing.");

            // If episode element is missing return null
            var episodeXml = dataXml.Element("Episode");
            if (episodeXml == null) throw new ParseException("Error while parsing episode xml document. Xml Element 'Episode' is missing.");

            return Parse(episodeXml);
        }

        /// <summary>
        /// Parse episode xml element and returns null if xml is not valid
        /// </summary>
        /// <param name="episodeXml">Episode xml element</param>
        /// <returns>Return parsed episode or null if xml is not valid</returns>
        public Episode Parse(XElement episodeXml)
        {
            if (episodeXml == null) throw new ArgumentNullException(nameof(episodeXml));

            // If episode has no id or number skip parsing and return null
            var id = episodeXml.ElementAsUInt("id");
            if (!id.HasValue) throw new ParseException("Error while parsing an episode xml element. Id is missing.");

            var number = episodeXml.ElementAsInt("EpisodeNumber");
            if (!number.HasValue) throw new ParseException("Error while parsing an episode xml element. EpisodeNumber is missing.");

            return new Episode(id.Value)
            {
                SeasonId = episodeXml.ElementAsUInt("seasonid"),
                SeasonNumber = episodeXml.ElementAsUInt("SeasonNumber"),
                FirstAired = episodeXml.ElementAsDateTime("FirstAired"),
                Number = number.Value,
                Title = episodeXml.ElementAsString("EpisodeName"),
                Directors = episodeXml.ElementAsString("Director").SplitByPipe(),
                GuestStars = episodeXml.ElementAsString("GuestStars").SplitByPipe(),
                Description = episodeXml.ElementAsString("Overview", true),
                Rating = episodeXml.ElementAsDouble("Rating"),
                Writers = episodeXml.ElementAsString("Writer").SplitByPipe(),
                RatingCount = episodeXml.ElementAsInt("RatingCount"),
                ThumbWidth = episodeXml.ElementAsInt("thumb_width"),
                ThumbHeight = episodeXml.ElementAsInt("thumb_height"),
                ThumbRemotePath = episodeXml.ElementAsString("filename"),
                Language = episodeXml.ElementAsString("Language").ToLanguage(),
                LastUpdated = episodeXml.ElementFromEpochToDateTime("lastupdated")
            };
        }
    }
}
