using System.Collections.Generic;
using System.Threading.Tasks;
using TheTVDBSharp.Models;
using TheTVDBSharp.Services;

namespace TheTVDBSharp
{
    /// <summary>
    ///     The main class which will handle all user interaction.
    /// </summary>
    public class TheTVDBManager : ITheTVDBManager
    {
        private readonly IEpisodeService episodeService;
        private readonly ISeriesService seriesService;
        private readonly IUpdateService updateService;
        private readonly IBannerService bannerService;

        private readonly IActorParseService actorParseService;
        private readonly IBannerParseService bannerParseService;
        private readonly IEpisodeParseService episodeParseService;
        private readonly ISeriesParseService seriesParseService;
        private readonly IUpdateParseService updateParseService;

        /// <summary>
        ///     Creates a new instance with the provided API key and standard <see cref="IDataProvider" />.
        /// </summary>
        /// <param name="apiKey">The API key provided by TVDB.</param>
        public TheTVDBManager(string apiKey, string url = "http://thetvdb.com")
        {
            // Api Configuration
            var apiConfiguration = new ProxyConfiguration(apiKey, url);

            // Proxy Services
            this.seriesService = new SeriesServiceProxy(apiConfiguration);
            this.episodeService = new EpisodeServiceProxy(apiConfiguration);
            this.updateService = new UpdateServiceProxy(apiConfiguration);
            this.bannerService = new BannerServiceProxy(apiConfiguration);

            // Parse Services
            this.actorParseService = new ActorParseService();
            this.bannerParseService = new BannerParseService();
            this.episodeParseService = new EpisodeParseService();
            this.seriesParseService = new SeriesParseService(actorParseService, bannerParseService, episodeParseService);
            this.updateParseService = new UpdateParseService();
        }

        /// <summary>
        ///     Search for a show in the database.
        /// </summary>
        /// <param name="query">Query that identifies the show.</param>
        /// <param name="results">Maximal amount of results in the returning set. Default is 5.</param>
        /// <returns>Returns a list of shows.</returns>
        public async Task<IReadOnlyCollection<Series>> SearchSeries(string query, Language language)
        {
            var seriesCollectionRaw = await this.seriesService.Search(query, language);
            return this.seriesParseService.ParseSearch(seriesCollectionRaw);
        }

        /// <summary>
        ///     Get a specific series based on its ID and if compression mode is true also all banners and actors.
        /// </summary>
        /// <param name="showId">ID of the series.</param>
        /// <param name="language">Language of the series.</param>
        /// <param name="compression">Set compression mode to false if you want to have an uncompressed transmission 
        /// which increases the bandwith load a lot. Compressed transmission also loads all banners and actors.</param>
        /// <returns>Returns the corresponding show.</returns>
        public async Task<Series> GetSeries(uint showId, Language language, bool compression = true)
        {
            if (compression)
            {
                var fullSeriesStream = await this.seriesService.RetrieveFull(showId, language);
                return await this.seriesParseService.ParseFull(fullSeriesStream, language);
            }
            else
            {
                var seriesRaw = await this.seriesService.Retrieve(showId, language);
                return this.seriesParseService.Parse(seriesRaw);
            }
        }

        /// <summary>
        ///     Get a specific episode based on its ID.
        /// </summary>
        /// <param name="episodeId">ID of the episode</param>
        /// <param name="lang">ISO 639-1 language code for the episode</param>
        /// <returns>The corresponding episode</returns>
        public async Task<Episode> GetEpisode(uint episodeId, Language language)
        {
            var episodeRaw = await this.episodeService.Retrieve(episodeId, language);
            return this.episodeParseService.Parse(episodeRaw);
        }

        /// <summary>
        ///     Get all updates since a given interval.
        /// </summary>
        /// <param name="interval">The interval you need to retrieve. 
        /// E.g. if you have last updated your data the same day than you should set Interval.Day. 
        /// The higher the interval the higher the bandwidth costs are.</param>
        /// <param name="compression">Only set compress mode to false when you know what your are doing. 
        /// Disabled compression raises the bandwith costs a lot.</param>
        /// <returns>Returns the update container which consists of all update elements.</returns>
        public async Task<UpdateContainer> GetUpdates(Interval interval, bool compression = true)
        {
            if (compression)
            {
                var updateContainerStream = await this.updateService.Retrieve(interval);
                return this.updateParseService.Parse(updateContainerStream, interval);
            }
            else
            {
                var updateContainerRaw = await this.updateService.RetrieveUncompressed(interval);
                return this.updateParseService.ParseUncompressed(updateContainerRaw);
            }
        }

        /// <summary>
        ///     Get a specific banner based on its remote path.
        /// </summary>
        /// <param name="remotePath">The remote path of the banner which can be 
        /// found in the BannerBase or in the BannerUpdate model.</param>
        /// <returns>Returns the banner as byte array.</returns>
        public async Task<byte[]> GetBanner(string remotePath)
        {
            return await this.bannerService.Retrieve(remotePath);
        }
    }
}