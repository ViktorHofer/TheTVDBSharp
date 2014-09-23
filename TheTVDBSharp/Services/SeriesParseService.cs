using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using TheTVDBSharp.Models;

namespace TheTVDBSharp.Services
{
    public class SeriesParseService : ISeriesParseService
    {
        private readonly ISimpleLogger logger;
        private readonly IActorParseService actorParseService;
        private readonly IBannerParseService bannerParseService;
        private readonly IEpisodeParseService episodeParseService;

        public SeriesParseService(IActorParseService actorParseService,
            IBannerParseService bannerParseService,
            IEpisodeParseService episodeParseService,
            ISimpleLogger logger)
        {
            this.actorParseService = actorParseService;
            this.bannerParseService = bannerParseService;
            this.episodeParseService = episodeParseService;
            this.logger = logger;
        }

        /// <summary>
        /// Parse series xml document and returns null if xml is not valid
        /// </summary>
        /// <param name="seriesRaw">Series xml document</param>
        /// <returns>Returns the parsed series or null if xml is not valid</returns>
        public Series Parse(string seriesRaw)
        {
            if (seriesRaw == null) throw new ArgumentNullException("seriesRaw", "Series xml string cannot be null");

            // If xml cannot be created return null
            XDocument doc;
            try
            {
                doc = XDocument.Parse(seriesRaw);
            }
            catch (XmlException e)
            {
                this.logger.Log("Series string cannot be parsed into a xml document.", LogLevel.Error, e);
                return null;
            }

            // If Data element is missing return null
            var seriesXml = doc.Element("Data");
            if (seriesXml == null)
            {
                this.logger.Log("Error while parsing series xml document. Xml Element 'Data' is missing.", LogLevel.Error);
                return null;
            }

            // If Series element is missing return null
            var seriesMetaXml = seriesXml.Element("Series");
            if (seriesMetaXml == null)
            {
                this.logger.Log("Error while parsing series xml document. Xml Element 'Series' is missing.", LogLevel.Error);
                return null;
            }

            // Parsing series metadata
            var series = Parse(seriesMetaXml);
            if (series == null) return null;

            // Parsing episodes
            List<Episode> episodeList = new List<Episode>();
            foreach (var episodeXml in seriesXml.Elements("Episode"))
            {
                // If episode could not be parsed skip it and continue
                var episode = this.episodeParseService.Parse(episodeXml);
                if (episode != null) episodeList.Add(episode);
            }
            series.Episodes = episodeList;

            return series;
        }

        /// <summary>
        /// Parse series metadata as xml element and returns null if xml is not valid (series has no id) 
        /// </summary>
        /// <param name="seriesXml">Series metadata as xml element</param>
        /// <returns></returns>
        public Series Parse(XElement seriesXml)
        {
            if (seriesXml == null) throw new ArgumentNullException("seriesXml", "Series xml element cannot be null");

            // If series has no id skip parsing and return null
            var id = seriesXml.ElementAsUInt("id");
            if (!id.HasValue)
            {
                this.logger.Log("Error while parsing a series xml element. Id is missing.", LogLevel.Error);
                return null;
            }

            return new Series(id.Value)
            {
                ImdbId = seriesXml.ElementAsString("IMDB_ID"),
                Title = seriesXml.ElementAsString("SeriesName", true),
                Language = seriesXml.ElementAsString("Language").ToLanguage(),
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
        }

        /// <summary>
        /// Parse complete series from an compressed stream with a given language and return null if stream or xml is not valid
        /// </summary>
        /// <param name="fullSeriesCompressedStream">Complete series zip compressed stream</param>
        /// <param name="language">Series language</param>
        /// <returns>Return the parsed complete series or null if stream or xml is not valid</returns>
        public async Task<Series> ParseFull(Stream fullSeriesCompressedStream, Language language)
        {
            string seriesRaw = null;
            string actorsRaw = null;
            string bannersRaw = null;

            using (ZipArchive archive = new ZipArchive(fullSeriesCompressedStream, ZipArchiveMode.Read))
            {
                // Return null if series metadata cannot be retrieved from the compressed file.
                seriesRaw = archive.GetEntry(language.ToShort() + ".xml").ReadToEnd();
                if (seriesRaw == null) return null;

                actorsRaw = archive.GetEntry("actors.xml").ReadToEnd();
                bannersRaw = archive.GetEntry("banners.xml").ReadToEnd();
            }

            // Create parse tasks if string not null
            var seriesTask = Task.Run(() => this.Parse(seriesRaw));
            var actorsTask = actorsRaw != null ?
                Task.Run(() => actorParseService.Parse(actorsRaw)) :
                null;
            var bannersTask = bannersRaw != null ?
                Task.Run(() => bannerParseService.Parse(bannersRaw)) :
                null;

            // Create tasks list to await it
            List<Task> tasks = new List<Task>() { seriesTask };
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
            if (seriesCollectionRaw == null) throw new ArgumentNullException("seriesCollectionRaw", "Search collection as string cannot be null");

            // If xml cannot be created return null
            XDocument doc;
            try
            {
                doc = XDocument.Parse(seriesCollectionRaw);
            }
            catch (XmlException e)
            {
                this.logger.Log("Search series collection string cannot be parsed into a xml document.", LogLevel.Error, e);
                return null;
            }

            // If Data element is missing return null
            var seriesCollectionXml = doc.Element("Data");
            if (seriesCollectionXml == null)
            {
                this.logger.Log("Error while parsing series xml document. Xml Element 'Data' is missing.", LogLevel.Error);
                return null;
            }

            List<Series> seriesList = new List<Series>();
            foreach (var seriesXml in seriesCollectionXml.Elements("Series"))
            {
                // If series could not be parsed skip it and continue
                var series = Parse(seriesXml);
                if (series != null) seriesList.Add(series);
            }

            return seriesList;
        }
    }
}
