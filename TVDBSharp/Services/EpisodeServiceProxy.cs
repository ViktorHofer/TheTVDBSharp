using System;
using System.Net.Http;
using System.Threading.Tasks;
using TVDBSharp.Models;

namespace TVDBSharp.Services
{
    public class EpisodeServiceProxy : ProxyBase, IEpisodeService
    {
        private const string episodeUrlFormat = "{0}/{1}/episodes/{2}/{3}.xml";

        public EpisodeServiceProxy(IProxyConfiguration config)
            : base(config)
        {
        }

        public async Task<string> Retrieve(int episodeId, Language language)
        {
            var url = new Uri(string.Format(episodeUrlFormat, base.proxyConfiguration.BaseUrl, base.proxyConfiguration.ApiKey, episodeId, language.ToShort()));

            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(url);
            }
        }
    }
}
