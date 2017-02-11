using System.Threading.Tasks;
using TheTVDBSharp.Services;
using TheTVDBSharp.Services.Libs;
using Xunit;

namespace TheTVDBSharp.Tests.Services
{
    public class EpisodeServiceProxyTest
    {
        readonly IEpisodeService _episodeService = new EpisodeServiceProxy(GlobalConfiguration.ApiConfiguration);

        [Fact]
        public async Task Retrieve_Episode_306213_Test()
        {
            var realEpisodeRaw = await _episodeService.Retrieve(306213, Models.Language.English);
            Assert.NotNull(realEpisodeRaw);
        }
    }
}
