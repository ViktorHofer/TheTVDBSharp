using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using TheTVDBSharp.Models;

namespace TheTVDBSharp.Services
{
    public class SeriesServiceProxy : ProxyBase, ISeriesService
    {
        private const string fullSeriesUrlFormat = "{0}/api/{1}/series/{2}/all/{3}.zip";
        private const string seriesUrlFormat = "{0}/api/{1}/series/{2}/all/{3}.xml";
        private const string searchSeriesUrlFormat = "{0}/api/GetSeries.php?seriesname={1}&language={2}";

        public SeriesServiceProxy(IProxyConfiguration config)
            : base(config)
        {
        }

        public async Task<Stream> RetrieveFull(uint showID, Language language)
        {
            var url = new Uri(string.Format(fullSeriesUrlFormat, base.proxyConfiguration.BaseUrl, base.proxyConfiguration.ApiKey, showID, language.ToShort()));

            using (var client = new HttpClient())
            {
                var message = await client.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                return await message.Content.ReadAsStreamAsync();
            }
        }

        public async Task<string> Retrieve(uint showId, Language language)
        {
            var url = new Uri(string.Format(seriesUrlFormat, base.proxyConfiguration.BaseUrl, base.proxyConfiguration.ApiKey, showId, language.ToShort()));

            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(url);
            }
        }

        public async Task<string> Search(string query, Language language)
        {
            var url = new Uri(string.Format(searchSeriesUrlFormat, base.proxyConfiguration.BaseUrl, query, language.ToShort()));

            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(url);
            }
        }
    }
}
