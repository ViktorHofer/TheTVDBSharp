using System;
using System.Threading.Tasks;
using TheTVDBSharp.Services.Libs;

namespace TheTVDBSharp.Services
{
    public class BannerServiceProxy : ProxyBase, IBannerService
    {
        private const string BannerUrlFormat = "{0}/banners/{1}";

        public BannerServiceProxy(IProxyConfiguration proxyConfiguration) 
            : base(proxyConfiguration)
        {
        }

#if PORTABLE
        public async Task<System.IO.Stream> Retrieve(string remotePath)
#elif WINDOWS_UAP
        public async Task<Windows.Storage.Streams.IInputStream> Retrieve(string remotePath)
#endif
        {
            var url = string.Format(BannerUrlFormat, 
                ProxyConfiguration.BaseUrl, 
                remotePath);

            var response = await GetAsync(url);

#if PORTABLE
            return await response.Content.ReadAsStreamAsync();
#elif WINDOWS_UAP
            return await response.Content.ReadAsInputStreamAsync();
#endif
        }
    }
}
