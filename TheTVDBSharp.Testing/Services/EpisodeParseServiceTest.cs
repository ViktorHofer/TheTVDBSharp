using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using TheTVDBSharp.Services;

namespace TheTVDBSharp.Testing.Services
{
    [TestClass]
    public class EpisodeParseServiceTest
    {
        private readonly IEpisodeParseService episodeParseService = new EpisodeParseService(GlobalConfiguration.Logger);

        [TestMethod]
        public void Parse_Episode_306213_Test()
        {            
            var sampleEpisodeRaw = SampleDataHelper.Open(SampleDataHelper.SampleData.Episode_306213);
            var episode = episodeParseService.Parse(sampleEpisodeRaw);
            
            Assert.IsNotNull(episode);
            Assert.AreEqual((uint)306213, episode.Id);
            Assert.AreEqual(5, episode.GuestStars.Count);
            Assert.AreEqual(1, episode.Writers.Count);
            Assert.AreEqual(1, episode.Directors.Count);
        }
    }
}
