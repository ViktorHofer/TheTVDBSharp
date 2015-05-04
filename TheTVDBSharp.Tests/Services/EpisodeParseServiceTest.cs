using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Threading.Tasks;
using TheTVDBSharp.Services;
using TheTVDBSharp.Services.Libs;

namespace TheTVDBSharp.Tests.Services
{
    [TestClass]
    public class EpisodeParseServiceTest
    {
        private readonly IEpisodeParseService _episodeParseService = new EpisodeParseService();

        [TestMethod]
        public async Task Parse_Episode_306213_Test()
        {            
            var sampleEpisodeRaw = await SampleDataHelper.GetTextAsync(SampleDataHelper.SampleData.Episode306213);
            var episode = _episodeParseService.Parse(sampleEpisodeRaw);
            
            Assert.IsNotNull(episode);
            Assert.AreEqual((uint)306213, episode.Id);
            Assert.AreEqual(5, episode.GuestStars.Count);
            Assert.AreEqual(1, episode.Writers.Count);
            Assert.AreEqual(1, episode.Directors.Count);
        }
    }
}
