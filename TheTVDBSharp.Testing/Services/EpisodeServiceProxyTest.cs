using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Xml.Linq;
using TheTVDBSharp.Services;

namespace TheTVDBSharp.Testing.Services
{
    [TestClass]
    public class EpisodeServiceProxyTest
    {
        IEpisodeService episodeService = new EpisodeServiceProxy(GlobalConfiguration.ApiConfiguration);

        [TestMethod]
        public async Task Retrieve_Episode_306213_Test()
        {
            var realEpisodeRaw = await episodeService.Retrieve(306213, Models.Language.English);
            Assert.IsNotNull(realEpisodeRaw);
        }
    }
}
