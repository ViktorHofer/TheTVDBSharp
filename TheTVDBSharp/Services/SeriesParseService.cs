using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using TheTVDBSharp.Models;

namespace TheTVDBSharp.Services
{
    public class SeriesParseService : ISeriesParseService
    {
        private readonly IActorParseService _actorParseService;
        private readonly IBannerParseService _bannerParseService;
        private readonly IEpisodeParseService _episodeParseService;

        public SeriesParseService(IActorParseService actorParseService,
            IBannerParseService bannerParseService,
            IEpisodeParseService episodeParseService)
        {
            if (actorParseService == null) throw new ArgumentNullException("actorParseService");
            if (bannerParseService == null) throw new ArgumentNullException("bannerParseService");
            if (episodeParseService == null) throw new ArgumentNullException("episodeParseService");

            _actorParseService = actorParseService;
            _bannerParseService = bannerParseService;
            _episodeParseService = episodeParseService;
        }

        /// <summary>
        /// Parse series xml document and returns null if xml is not valid
        /// </summary>
        /// <param name="seriesRaw">Series xml document</param>
        /// <returns>Returns the parsed series or null if xml is not valid</returns>
        public Series Parse(string seriesRaw)
        {
            if (seriesRaw == null) throw new ArgumentNullException("seriesRaw");

            // If xml cannot be created return null
            XDocument doc;
            try
            {
                doc = XDocument.Parse(seriesRaw);
            }
            catch (XmlException e)
            {
                throw new ParseException("Series string cannot be parsed into a xml document.", e);
            }

            // If Data element is missing return null
            var seriesXml = doc.Element("Data");
            if (seriesXml == null) throw new ParseException("Error while parsing series xml document. Xml element 'Data' is missing.");

            // If Series element is missing return null
            var seriesMetaXml = seriesXml.Element("Series");
            if (seriesMetaXml == null) throw new ParseException("Error while parsing series xml document. Xml Element 'Series' is missing.");

            // Parsing series metadata...
            // If parsing failed a ParseException will be thrown in the Parse method.
            var series = Parse(seriesMetaXml);

            // Parsing episodes
            series.Episodes = seriesXml.Elements("Episode")
                .Select(episodeXml => _episodeParseService.Parse(episodeXml))
                .Where(episode => episode != null)
                .ToList();

            return series;
        }

        /// <summary>
        /// Parse series metadata as xml element and returns null if xml is not valid (series has no id) 
        /// </summary>
        /// <param name="seriesXml">Series metadata as xml element</param>
        /// <param name="isSearchElement"></param>
        /// <returns>Returns the successfully parsed series</returns>
        public Series Parse(XElement seriesXml, bool isSearchElement = false)
        {
            if (seriesXml == null) throw new ArgumentNullException("seriesXml");

            // If series has no id throw ParseException
            var id = seriesXml.ElementAsUInt("id");
            if (!id.HasValue) throw new ParseException("Error while parsing a series xml element. Id is missing.");

            var series = new Series(id.Value)
            {
                ImdbId = seriesXml.ElementAsString("IMDB_ID"),
                Title = seriesXml.ElementAsString("SeriesName", true),
                Language = seriesXml.ElementAsString(isSearchElement ? "language" : "Language").ToLanguage(),
                Network = seriesXml.ElementAsString("Network"),
                Description = seriesXml.ElementAsString("Overview", true),
                Rating = seriesXml.ElementAsDouble("Rating"),
                RatingCount = seriesXml.ElementAsInt("RatingCount"),
                Runtime = seriesXml.ElementAsInt("Runtime"),
                BannerRemotePath = seriesXml.ElementAsString("banner"),
                FanartRemotePath = seriesXml.ElementAsString("fanart"),
                LastUpdated = seriesXml.ElementFromEpochToDateTime("lastupdated"),
                PosterRemotePath = seriesXml.ElementAsString("poster"),
                Zap2ItId = seriesXml.ElementAsString("zap2it_id"),
                FirstAired = seriesXml.ElementAsDateTime("FirstAired"),
                AirTime = seriesXml.ElementAsTimeSpan("Airs_Time"),
                AirDay = seriesXml.ElementAsEnum<Frequency>("Airs_DayOfWeek"),
                Status = seriesXml.ElementAsEnum<Status>("Status"),
                ContentRating = seriesXml.ElementAsString("ContentRating").ToContentRating(),
                Genres = seriesXml.ElementAsString("Genre").SplitByPipe()
            };

            if (series.FirstAired.HasValue)
            {
                series.Title = series.Title.Replace(string.Format(" ({0})", series.FirstAired.Value.Year), "");
            }

            return series;
        }

        /// <summary>
        /// Parse complete series from an compressed stream with a given language and return null if stream or xml is not valid
        /// </summary>
        /// <param name="fullSeriesCompressedStream">Complete series zip compressed stream</param>
        /// <param name="language">Series language</param>
        /// <returns>Return the parsed complete series or null if stream or xml is not valid</returns>
        public async Task<Series> ParseFull(Stream fullSeriesCompressedStream, Language language)
        {
            string seriesRaw;
            string actorsRaw;
            string bannersRaw;

            using (var archive = new ZipArchive(fullSeriesCompressedStream, ZipArchiveMode.Read))
            {
                // Throw ParseException if series metadata cannot be retrieved from the compressed file.
                seriesRaw = archive.GetEntry(language.ToShort() + ".xml").ReadToEnd();
                if (seriesRaw == null) throw new ParseException("Compressed file does not have a series metadata file.");

                actorsRaw = archive.GetEntry("actors.xml").ReadToEnd();
                bannersRaw = archive.GetEntry("banners.xml").ReadToEnd();
            }

            // Create parse tasks if string not null
            var seriesTask = Task.Run(() => Parse(seriesRaw));
            var actorsTask = actorsRaw != null ?
                Task.Run(() => _actorParseService.Parse(actorsRaw)) :
                null;
            var bannersTask = bannersRaw != null ?
                Task.Run(() => _bannerParseService.Parse(bannersRaw)) :
                null;

            // Create tasks list to await it
            var tasks = new List<Task> { seriesTask };
            if (actorsTask != null) tasks.Add(actorsTask);
            if (bannersTask != null) tasks.Add(bannersTask);

            await Task.WhenAll(tasks);

            var series = seriesTask.Result;
            if (actorsTask != null) series.Actors = actorsTask.Result;
            if (bannersTask != null) series.Banners = bannersTask.Result;

            return series;
        }

        /// <summary>
        /// Parse search series collection xml document as string and return null if xml is not valid
        /// </summary>
        /// <param name="seriesCollectionRaw">Series collection xml document as string</param>
        /// <returns>Return the parsed series collection or null if xml is not valid</returns>
        public IReadOnlyCollection<Series> ParseSearch(string seriesCollectionRaw)
        {
            if (seriesCollectionRaw == null) throw new ArgumentNullException("seriesCollectionRaw");

            // If xml cannot be created throw ParseException
            XDocument doc;
            try
            {
                doc = XDocument.Parse(seriesCollectionRaw);
            }
            catch (XmlException e)
            {
                throw new ParseException("Search series collection string cannot be parsed into a xml document.", e);
            }

            // If Data element is missing return null
            var seriesCollectionXml = doc.Element("Data");
            if (seriesCollectionXml == null) throw new ParseException("Error while parsing series xml document. Xml Element 'Data' is missing.");

            return seriesCollectionXml.Elements("Series")
                .Select(seriesXml => Parse(seriesXml, true))
                .Where(series => series != null)
                .ToList();
        }
    }
}
