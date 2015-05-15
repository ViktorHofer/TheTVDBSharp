using System;
using System.Threading.Tasks;
using TheTVDBSharp.Models;
using TheTVDBSharp.Services.Libs;

namespace TheTVDBSharp.Services
{
    public class SeriesServiceProxy : ProxyBase, ISeriesService
    {
        private const string FullSeriesUrlFormat = "{0}/api/{1}/series/{2}/all/{3}.zip";
        private const string SeriesUrlFormat = "{0}/api/{1}/series/{2}/all/{3}.xml";
        private const string SearchSeriesUrlFormat = "{0}/api/GetSeries.php?seriesname={1}&language={2}";

        public SeriesServiceProxy(IApiConfiguration apiConfiguration)
            : base(apiConfiguration)
        {
        }

#if WINDOWS_PORTABLE
        public async Task<System.IO.Stream> RetrieveFull(uint showId, Language language)
#elif WINDOWS_RUNTIME
        public async Task<Windows.Storage.Streams.IInputStream> RetrieveFull(uint showId, Language language)
#endif
        {
            var url = string.Format(FullSeriesUrlFormat, 
                ProxyConfiguration.BaseUrl, 
                ProxyConfiguration.ApiKey, 
                showId, 
                language.ToShort());

            var response = await GetAsync(url);

#if WINDOWS_PORTABLE
            return await response.Content.ReadAsStreamAsync();
#elif WINDOWS_RUNTIME
            return await response.Content.ReadAsInputStreamAsync();
#endif
        }

        public async Task<string> Retrieve(uint showId, Language language)
        {
            var url = string.Format(SeriesUrlFormat, 
                ProxyConfiguration.BaseUrl, 
                ProxyConfiguration.ApiKey, 
                showId, 
                language.ToShort());

            var response = await GetAsync(url);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> Search(string query, Language language)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            var url = string.Format(SearchSeriesUrlFormat, 
                ProxyConfiguration.BaseUrl, 
                query, 
                language.ToShort());

            var response = await GetAsync(url);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
