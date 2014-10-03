using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheTVDBSharp.Services;

namespace TheTVDBSharp.Testing.Services
{
    [TestClass]
    public class EpisodeParseServiceTest
    {
        private readonly IEpisodeParseService _episodeParseService = new EpisodeParseService();

        [TestMethod]
        public void Parse_Episode_306213_Test()
        {            
            var sampleEpisodeRaw = SampleDataHelper.Open(SampleDataHelper.SampleData.Episode306213);
            var episode = _episodeParseService.Parse(sampleEpisodeRaw);
            
            Assert.IsNotNull(episode);
            Assert.AreEqual((uint)306213, episode.Id);
            Assert.AreEqual(5, episode.GuestStars.Count);
            Assert.AreEqual(1, episode.Writers.Count);
            Assert.AreEqual(1, episode.Directors.Count);
        }
    }
}
