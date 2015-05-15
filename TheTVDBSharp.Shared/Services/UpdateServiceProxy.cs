using System;
using System.Threading.Tasks;
using TheTVDBSharp.Models;
using TheTVDBSharp.Services.Libs;

namespace TheTVDBSharp.Services
{
    public class UpdateServiceProxy : ProxyBase, IUpdateService
    {
        private const string UpdateCompressedUrlFormat = "{0}/api/{1}/updates/updates_{2}.zip";
        private const string UpdateUncompressedUrlFormat = "{0}/api/{1}/updates/updates_{2}.xml";

        public UpdateServiceProxy(IApiConfiguration apiConfiguration)
            : base(apiConfiguration)
        {
        }

#if WINDOWS_PORTABLE
        public async Task<System.IO.Stream> Retrieve(Interval interval)
#elif WINDOWS_RUNTIME
        public async Task<Windows.Storage.Streams.IInputStream> Retrieve(Interval interval)
#endif
        {
            var url = string.Format(UpdateCompressedUrlFormat, 
                ProxyConfiguration.BaseUrl, 
                ProxyConfiguration.ApiKey, 
                interval.ToApiString());

            var response = await GetAsync(url);

#if WINDOWS_PORTABLE
            return await response.Content.ReadAsStreamAsync();
#elif WINDOWS_RUNTIME
            return await response.Content.ReadAsInputStreamAsync();
#endif
        }

        public async Task<string> RetrieveUncompressed(Interval interval)
        {
            var url = string.Format(UpdateUncompressedUrlFormat, 
                ProxyConfiguration.BaseUrl, 
                ProxyConfiguration.ApiKey, 
                interval.ToApiString());

            var response = await GetAsync(url);
            
            return await response.Content.ReadAsStringAsync();
        }
    }
}
