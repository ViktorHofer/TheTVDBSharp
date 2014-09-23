using System.IO;
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
            var url = string.Format(updateCompressedUrlFormat, base.proxyConfiguration.BaseUrl, base.proxyConfiguration.ApiKey, interval.ToApiString());

            var response = await base.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStreamAsync();
        }

        public async Task<string> RetrieveUncompressed(Interval interval)
        {
            var url = string.Format(updateUncompressedUrlFormat, base.proxyConfiguration.BaseUrl, base.proxyConfiguration.ApiKey, interval.ToApiString());

            var response = await base.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
