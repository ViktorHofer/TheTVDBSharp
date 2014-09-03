using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using TVDBSharp.Dtos.Data;
using TVDBSharp.Models;
using TVDBSharp.Services;
using TVDBSharp.Models;
using TVDBSharp.Services.Episode;
using System.Linq;

namespace TVDBSharp.Services.Episode
{
    public class _EpisodeService : _IEpisodeService
    {
        #region Fields

        private readonly IEpisodeService episodeProvider;

        #endregion

        #region Constructor

        public _EpisodeService(IEpisodeService episodeProvider)
        {
            this.episodeProvider = episodeProvider;
        }

        #endregion

        #region IEpisodeService

        public async Task<Episode> Retrieve(int episodeId, Language language)
        {
            var data = await this.episodeProvider.Retrieve(episodeId, language);
            return this.Parse(data);
        }

        public Episode Parse(Stream stream)
        {
            var doc = XDocument.Load(stream);
            var x = doc.Root.Element("Episode")
                .Descendants()
                .Where(e => !e.IsEmpty && !string.IsNullOrWhiteSpace(e.Value));

            return new Episode()
            {
                //Description = data.Episode.Overview,
                //Directors = data.Episode.Director.SplitByPipe(),
                //FirstAired = data.Episode.FirstAired,
                //GuestStars = data.Episode.GuestStars.SplitByPipe(),
                //Language = data.Episode.Language.ToLanguage(),
                //LastUpdated = data.Episode.lastupdated.ToDateTime(),
                //Number = data.Episode.EpisodeNumber,
                //Rating = data.Episode.Rating,
                //RatingCount = data.Episode.RatingCount,
                //ThumbHeight = data.Episode.thumb_height,
                //ThumbWidth = data.Episode.thumb_width,
                //ThumbRemotePath = data.Episode.filename,
                //Title = data.Episode.EpisodeName,
                //Writers = data.Episode.Writer.SplitByPipe()
            };
        }

        private static string GetElementValue(XContainer element, string name)
        {
            if ((element == null) || (element.Element(name) == null))
                return String.Empty;
            return element.Element(name).Value;
        }

        #endregion
    }
}
