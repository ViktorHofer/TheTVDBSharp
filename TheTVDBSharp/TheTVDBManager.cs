using System;
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

        private readonly IActorParseService actorParseService;
        private readonly IBannerParseService bannerParseService;
        private readonly IEpisodeParseService episodeParseService;
        private readonly ISeriesParseService seriesParseService;


        /// <summary>
        ///     Creates a new instance with the provided API key and standard <see cref="IDataProvider" />.
        /// </summary>
        /// <param name="apiKey">The API key provided by TVDB.</param>
        public TheTVDBManager(string apiKey, string url = "http://thetvdb.com/api")
        {
            // Api Configuration
            var apiConfiguration = new ProxyConfiguration(apiKey, url);

            // Proxy Services
            this.seriesService = new SeriesServiceProxy(apiConfiguration);
            this.episodeService = new EpisodeServiceProxy(apiConfiguration);

            // Parse Services
            this.actorParseService = new ActorParseService();
            this.bannerParseService = new BannerParseService();
            this.episodeParseService = new EpisodeParseService();
            this.seriesParseService = new SeriesParseService(actorParseService, bannerParseService, episodeParseService);
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
        ///     Get a specific show based on its ID.
        /// </summary>
        /// <param name="showId">ID of the show.</param>
        /// <returns>Returns the corresponding show.</returns>
        public async Task<Series> GetSeries(uint showId, Language language)
        {
            var seriesRaw =  await this.seriesService.Retrieve(showId, language);
            return this.seriesParseService.Parse(seriesRaw);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="showId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public async Task<Series> GetFullSeries(uint showId, Language language)
        {
            var fullSeriesCompressedStream = await this.seriesService.RetrieveFull(showId, language);
            return await this.seriesParseService.ParseFull(fullSeriesCompressedStream, language);
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
    }
}