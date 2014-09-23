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
            var url = string.Format("{0}/banners/{1}", base.proxyConfiguration.BaseUrl, remotePath);

            var response = await base.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
