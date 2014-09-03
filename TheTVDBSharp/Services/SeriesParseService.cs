
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Xml.Linq;
using TheTVDBSharp.Models;
namespace TheTVDBSharp.Services
{
    public class SeriesParseService : ISeriesParseService
    {
        private readonly IActorParseService actorParseService;
        private readonly IBannerParseService bannerParseService;
        private readonly IEpisodeParseService episodeParseService;

        public SeriesParseService(IActorParseService actorParseService,
            IBannerParseService bannerParseService,
            IEpisodeParseService episodeParseService)
        {
            this.actorParseService = actorParseService;
            this.bannerParseService = bannerParseService;
            this.episodeParseService = episodeParseService;
        }

        public Series Parse(string seriesRaw)
        {
            var seriesDoc = XDocument.Parse(seriesRaw);
            var seriesXml = seriesDoc.Element("Data");
            var seriesMetaXml = seriesXml.Element("Series");

            // Parsing series metadata
            var series = Parse(seriesMetaXml);

            // If invalid series metadata then return
            if (series == null) return null;

            // Parsing episodes
            List<Episode> episodeList = new List<Episode>();
            foreach (var episodeXml in seriesXml.Elements("Episode"))
            {
                var episode = this.episodeParseService.Parse(episodeXml);
                episodeList.Add(episode);
            }
            series.Episodes = episodeList;

            return series;
        }

        public Series Parse(XElement seriesXml)
        {
            var id = seriesXml.ElementAsUInt("id");

            if (!id.HasValue) return null;

            var x = new Series(id.Value)
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
            return x;
        }

        public async Task<Series> ParseFull(Stream fullSeriesCompressedStream, Language language)
        {
            string seriesRaw = null;
            string actorsRaw = null;
            string bannersRaw = null;

            using (ZipArchive archive = new ZipArchive(fullSeriesCompressedStream, ZipArchiveMode.Read))
            {
                seriesRaw = ReadToEnd(archive.GetEntry(language.ToShort() + ".xml"));
                actorsRaw = ReadToEnd(archive.GetEntry("actors.xml"));
                bannersRaw = ReadToEnd(archive.GetEntry("banners.xml"));
            }

            var seriesTask = Task.Run(() =>
            {
                return Parse(seriesRaw);
            });

            var actorsTask = Task.Run(() =>
            {
                return actorParseService.Parse(actorsRaw);
            });

            var bannersTask = Task.Run(() => 
            {
                return bannerParseService.Parse(bannersRaw);
            });                

            await Task.WhenAll(seriesTask, actorsTask, bannersTask);

            var series = seriesTask.Result;
            series.Actors = actorsTask.Result;
            series.Banners = bannersTask.Result;

            return series;
        }

        public IReadOnlyCollection<Series> ParseSearch(string seriesCollectionRaw)
        {
            var seriesDoc = XDocument.Parse(seriesCollectionRaw);
            var seriesCollectionXml = seriesDoc.Element("Data");

            List<Series> seriesList = new List<Series>();
            foreach (var seriesXml in seriesCollectionXml.Elements("Series"))
            {
                var series = Parse(seriesXml);
                seriesList.Add(series);
            }

            return seriesList;
        }

        private string ReadToEnd(ZipArchiveEntry entry)
        {
            using (var stream = entry.Open())
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
