using System.Xml.Linq;
using TheTVDBSharp.Models;

namespace TheTVDBSharp.Services
{
    public class EpisodeParseService : IEpisodeParseService
    {
        public Episode Parse(string episodeRaw)
        {
            var doc = XDocument.Parse(episodeRaw);
            var episodeXml = doc.Element("Data").Element("Episode");
            return Parse(episodeXml);
        }

        public Episode Parse(XElement episodeXml)
        {
            var id = episodeXml.ElementAsUInt("id");
            var number = episodeXml.ElementAsInt("EpisodeNumber");

            if (!id.HasValue || !number.HasValue) return null;

            var episode = new Episode(id.Value)
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

            return episode;
        }
    }
}
