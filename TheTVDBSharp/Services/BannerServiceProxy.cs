using System;
using System.Net.Http;
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
            var url = new Uri(string.Format("{0}/banners/{1}", base.proxyConfiguration.BaseUrl, remotePath));

            using (var client = new HttpClient())
            {
                return await client.GetByteArrayAsync(url);
            }
        }
    }
}
