using System.Threading.Tasks;
using TheTVDBSharp.Models;

namespace TheTVDBSharp.Services
{
    public class EpisodeServiceProxy : ProxyBase, IEpisodeService
    {
        private const string episodeUrlFormat = "{0}/api/{1}/episodes/{2}/{3}.xml";

        public EpisodeServiceProxy(IProxyConfiguration config)
            : base(config)
        {
        }

        public async Task<string> Retrieve(uint episodeId, Language language)
        {
            var url = string.Format(episodeUrlFormat, base.proxyConfiguration.BaseUrl, base.proxyConfiguration.ApiKey, episodeId, language.ToShort());
            
            var response = await base.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
