using System.Threading.Tasks;
using TheTVDBSharp.Models;

namespace TheTVDBSharp.Services
{
    public class EpisodeServiceProxy : ProxyBase, IEpisodeService
    {
        private const string EpisodeUrlFormat = "{0}/api/{1}/episodes/{2}/{3}.xml";

        public EpisodeServiceProxy(IProxyConfiguration config)
            : base(config)
        {
        }

        public async Task<string> Retrieve(uint episodeId, Language language)
        {
            var url = string.Format(EpisodeUrlFormat, ProxyConfiguration.BaseUrl, ProxyConfiguration.ApiKey, episodeId, language.ToShort());
            
            var response = await GetAsync(url);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
