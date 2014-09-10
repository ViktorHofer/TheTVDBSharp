using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using TheTVDBSharp.Models;

namespace TheTVDBSharp.Services
{
    public class UpdateServiceProxy : ProxyBase, IUpdateService
    {
        private const string updateCompressedUrlFormat = "{0}/api/{1}/updates/updates_{2}.zip";
        private const string updateUncompressedUrlFormat = "{0}/api/{1}/updates/updates_{2}.xml";

        public UpdateServiceProxy(IProxyConfiguration config)
            : base(config)
        {
        }

        public async Task<Stream> Retrieve(Interval interval)
        {
            var url = new Uri(string.Format(updateCompressedUrlFormat, base.proxyConfiguration.BaseUrl, base.proxyConfiguration.ApiKey, interval.ToApiString()));

            using (var client = new HttpClient())
            {
                var message = await client.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                return await message.Content.ReadAsStreamAsync();
            }
        }

        public async Task<string> RetrieveUncompressed(Interval interval)
        {
            var url = new Uri(string.Format(updateUncompressedUrlFormat, base.proxyConfiguration.BaseUrl, base.proxyConfiguration.ApiKey, interval.ToApiString()));

            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(url);
            }
        }
    }
}
