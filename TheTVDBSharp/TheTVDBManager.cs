using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheTVDBSharp.Models;
using TheTVDBSharp.Services;

namespace TheTVDBSharp
{
    /// <summary>
    /// The main class which will handle all user interaction
    /// </summary>
    public class TheTVDBManager : ITheTVDBManager
    {
        private readonly ISimpleLogger logger;

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
        /// Creates a new instance with the provided API key and a base api url
        /// </summary>
        /// <param name="apiKey">The API key provided by TVDB</param>
        /// <param name="url">The API base url</param>
        public TheTVDBManager(string apiKey, string url = "http://thetvdb.com")
        {
            // Logging
            logger = new SimpleLogger();
            logger.Logged += (s, e) =>
            {
                if (this.Logged != null)
                {
                    this.Logged(s, e);
                }
            };

            // Api Configuration
            var apiConfiguration = new ProxyConfiguration(apiKey, url);

            // Proxy Services
            this.seriesService = new SeriesServiceProxy(apiConfiguration);
            this.episodeService = new EpisodeServiceProxy(apiConfiguration);
            this.updateService = new UpdateServiceProxy(apiConfiguration);
            this.bannerService = new BannerServiceProxy(apiConfiguration);

            // Parse Services
            this.actorParseService = new ActorParseService(this.logger);
            this.bannerParseService = new BannerParseService(this.logger);
            this.episodeParseService = new EpisodeParseService(this.logger);
            this.seriesParseService = new SeriesParseService(actorParseService, bannerParseService, episodeParseService, this.logger);
            this.updateParseService = new UpdateParseService(this.logger);
        }

        /// <summary>
        /// Logging communication interface
        /// </summary>
        public event EventHandler<LogEventArgs> Logged;

        /// <summary>
        /// Search for a series with a given query and a language and returns null if api response is not well formed
        /// </summary>
        /// <param name="query">Query that identifies the series.</param>
        /// <param name="language">Series language.</param>
        /// <returns>Returns a readonly collection of series or null if response is not well formed</returns>
        public async Task<IReadOnlyCollection<Series>> SearchSeries(string query, Language language)
        {
            var seriesCollectionRaw = await this.seriesService.Search(query, language);
            if (seriesCollectionRaw == null) return null;

            return this.seriesParseService.ParseSearch(seriesCollectionRaw);
        }

        /// <summary>
        /// Get a specific series based on its id and if compression mode is true also all banners and actors; 
        /// or null if api response is not well formed
        /// </summary>
        /// <param name="seriesId">Id of the series.</param>
        /// <param name="language">Language of the series.</param>
        /// <param name="compression">Set compression mode to false if you want to have an uncompressed transmission 
        /// which increases the bandwith load a lot. Compressed transmission also loads all banners and actors.</param>
        /// <returns>Returns the corresponding series or null if api response is not well formed</returns>
        public async Task<Series> GetSeries(uint seriesId, Language language, bool compression = true)
        {
            if (compression)
            {
                var fullSeriesStream = await this.seriesService.RetrieveFull(seriesId, language);
                if (fullSeriesStream == null) return null;

                return await this.seriesParseService.ParseFull(fullSeriesStream, language);
            }
            else
            {
                var seriesRaw = await this.seriesService.Retrieve(seriesId, language);
                if (seriesRaw == null) return null;

                return this.seriesParseService.Parse(seriesRaw);
            }
        }

        /// <summary>
        /// Get a specific episode based on its id and the given language and returns null if api response is not well formed
        /// </summary>
        /// <param name="episodeId">Id of the episode</param>
        /// <param name="language">Episode language</param>
        /// <returns>The corresponding episode or null if api response is not well formed</returns>
        public async Task<Episode> GetEpisode(uint episodeId, Language language)
        {
            var episodeRaw = await this.episodeService.Retrieve(episodeId, language);
            if (episodeRaw == null) return null;

            return this.episodeParseService.Parse(episodeRaw);
        }

        /// <summary>
        /// Get all updates since a given interval.
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
                if (updateContainerStream == null) return null;

                return this.updateParseService.Parse(updateContainerStream, interval);
            }
            else
            {
                var updateContainerRaw = await this.updateService.RetrieveUncompressed(interval);
                if (updateContainerRaw == null) return null;

                return this.updateParseService.ParseUncompressed(updateContainerRaw);
            }
        }

        /// <summary>
        ///  Get a specific banner based on its remote path.
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