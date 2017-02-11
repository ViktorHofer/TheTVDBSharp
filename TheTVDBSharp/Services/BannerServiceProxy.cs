using System;
using System.Threading.Tasks;
using TheTVDBSharp.Models;
using TheTVDBSharp.Services.Libs;

namespace TheTVDBSharp.Services
{
    public class BannerServiceProxy : ProxyBase, IBannerService
    {
        private const string BannerUrlFormat = "{0}/banners/{1}";

        public BannerServiceProxy(IApiConfiguration apiConfiguration) 
            : base(apiConfiguration)
        {
        }
        
        public async Task<System.IO.Stream> Retrieve(string remotePath)
        {
            var url = string.Format(BannerUrlFormat, 
                ProxyConfiguration.BaseUrl, 
                remotePath);

            var response = await GetAsync(url);
            
            return await response.Content.ReadAsStreamAsync();
        }
    }
}
