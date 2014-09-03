using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Xml.Linq;
using TheTVDBSharp.Services;

namespace TheTVDBSharp.Testing.Services
{
    [TestClass]
    public class EpisodeServiceProxyTest
    {
        [TestMethod]
        public async Task Retrieve_Episode_306213_Test()
        {
            var episodeProvider = new EpisodeServiceProxy(GlobalConfiguration.ApiConfiguration);

            var realEpisodeRaw = await episodeProvider.Retrieve(306213, Models.Language.English);
            realEpisodeRaw = XDocument.Parse(realEpisodeRaw).ToString();

            var sampleEpisodeRaw = SampleDataHelper.Open(SampleDataHelper.SampleData.Episode_306213);
            sampleEpisodeRaw = XDocument.Parse(sampleEpisodeRaw).ToString();

            Assert.AreEqual(sampleEpisodeRaw, realEpisodeRaw);
        }
    }
}
