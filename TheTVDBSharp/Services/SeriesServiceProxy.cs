using System.IO;
using System.Threading.Tasks;
using TheTVDBSharp.Models;

namespace TheTVDBSharp.Services
{
    public class SeriesServiceProxy : ProxyBase, ISeriesService
    {
        private const string FullSeriesUrlFormat = "{0}/api/{1}/series/{2}/all/{3}.zip";
        private const string SeriesUrlFormat = "{0}/api/{1}/series/{2}/all/{3}.xml";
        private const string SearchSeriesUrlFormat = "{0}/api/GetSeries.php?seriesname={1}&language={2}";

        public SeriesServiceProxy(IProxyConfiguration config)
            : base(config)
        {
        }

        public async Task<Stream> RetrieveFull(uint showId, Language language)
        {
            var url = string.Format(FullSeriesUrlFormat, ProxyConfiguration.BaseUrl, ProxyConfiguration.ApiKey, showId, language.ToShort());

            var response = await GetAsync(url);

            return await response.Content.ReadAsStreamAsync();
        }

        public async Task<string> Retrieve(uint showId, Language language)
        {
            var url = string.Format(SeriesUrlFormat, ProxyConfiguration.BaseUrl, ProxyConfiguration.ApiKey, showId, language.ToShort());

            var response = await GetAsync(url);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> Search(string query, Language language)
        {
            var url = string.Format(SearchSeriesUrlFormat, ProxyConfiguration.BaseUrl, query, language.ToShort());

            var response = await GetAsync(url);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
