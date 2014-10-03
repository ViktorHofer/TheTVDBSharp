using System.Threading.Tasks;

namespace TheTVDBSharp.Services
{
    public class BannerServiceProxy : ProxyBase, IBannerService
    {
        public BannerServiceProxy(IProxyConfiguration proxyConfiguration) 
            : base(proxyConfiguration)
        {
        }

        public async Task<byte[]> Retrieve(string remotePath)
        {
            var url = string.Format("{0}/banners/{1}", ProxyConfiguration.BaseUrl, remotePath);

            var response = await GetAsync(url);

            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
