using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheTVDBSharp.Models;
using TheTVDBSharp.Services;
using TheTVDBSharp.Services.Libs;

namespace TheTVDBSharp
{
    /// <summary>
    /// The main class which will handle all user interaction
    /// </summary>
    public class TheTvdbManager : ITheTvdbManager
    {
        private readonly IEpisodeService _episodeService;
        private readonly ISeriesService _seriesService;
        private readonly IUpdateService _updateService;
        private readonly IBannerService _bannerService;

        private readonly IEpisodeParseService _episodeParseService;
        private readonly ISeriesParseService _seriesParseService;
        private readonly IUpdateParseService _updateParseService;

        /// <summary>
        /// Creates a new instance with the provided api configuration
        /// </summary>
        /// <param name="apiConfiguration">The API configuration</param>
        public TheTvdbManager(IApiConfiguration apiConfiguration)
        {
            if (apiConfiguration == null)
                throw new ArgumentNullException(nameof(apiConfiguration));
            if (string.IsNullOrWhiteSpace(apiConfiguration.BaseUrl))
                throw new ArgumentOutOfRangeException(nameof(apiConfiguration), "Base url must be set");

            // Proxy Services
            _seriesService = new SeriesServiceProxy(apiConfiguration);
            _episodeService = new EpisodeServiceProxy(apiConfiguration);
            _updateService = new UpdateServiceProxy(apiConfiguration);
            _bannerService = new BannerServiceProxy(apiConfiguration);

            // Initialize parse services
            var actorParseService = new ActorParseService();
            var bannerParseService = new BannerParseService();
            _episodeParseService = new EpisodeParseService();
            _seriesParseService = new SeriesParseService(actorParseService, bannerParseService, _episodeParseService);
            _updateParseService = new UpdateParseService();
        }

        /// <summary>
        /// Creates a new instance with the provided API key and a base api url
        /// </summary>
        /// <param name="apiKey">The API key provided by TVDB</param>
        /// <param name="baseUrl">The API base url</param>
        public TheTvdbManager(string apiKey, string baseUrl = "http://thetvdb.com")
            : this(new ApiConfiguration(apiKey, baseUrl))
        {
        }

        /// <summary>
        /// Search for a series with a given query and a language and returns null if api response is not well formed
        /// </summary>
        /// <param name="query">Query that identifies the series.</param>
        /// <param name="language">Series language.</param>
        /// <returns>Returns a readonly collection of series or null if response is not well formed</returns>
        public async Task<IReadOnlyCollection<Series>> SearchSeries(string query, Language language)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            var seriesCollectionRaw = await _seriesService.Search(query, language);
            return _seriesParseService.ParseSearch(seriesCollectionRaw);
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
                var fullSeriesStream = await _seriesService.RetrieveFull(seriesId, language);
                return await _seriesParseService.ParseFull(fullSeriesStream, language);
            }
            
            var seriesRaw = await _seriesService.Retrieve(seriesId, language);
            return _seriesParseService.Parse(seriesRaw);
        }

        /// <summary>
        /// Get a specific episode based on its id and the given language and returns null if api response is not well formed
        /// </summary>
        /// <param name="episodeId">Id of the episode</param>
        /// <param name="language">Episode language</param>
        /// <returns>The corresponding episode or null if api response is not well formed</returns>
        public async Task<Episode> GetEpisode(uint episodeId, Language language)
        {
            var episodeRaw = await _episodeService.Retrieve(episodeId, language);
            return _episodeParseService.Parse(episodeRaw);
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
                var updateContainerStream = await _updateService.Retrieve(interval);
                return _updateParseService.Parse(updateContainerStream, interval);
            }
            
            var updateContainerRaw = await _updateService.RetrieveUncompressed(interval);
            return _updateParseService.ParseUncompressed(updateContainerRaw);
        }

        /// <summary>
        ///  Get a specific banner based on its remote path.
        /// </summary>
        /// <param name="remotePath">The remote path of the banner which can be 
        /// found in the BannerBase or in the BannerUpdate model.</param>
        /// <returns>Returns the banner as byte array.</returns>
#if WINDOWS_PORTABLE
        public async Task<System.IO.Stream> GetBanner(string remotePath)
#elif WINDOWS_RUNTIME
        public async Task<Windows.Storage.Streams.IInputStream> GetBanner(string remotePath)
#endif
        {
            if (remotePath == null) throw new ArgumentNullException(nameof(remotePath));

            return await _bannerService.Retrieve(remotePath);
        }
    }
}