using TheTVDBSharp.Services;
using TheTVDBSharp.Services.Libs;
using Xunit;

namespace TheTVDBSharp.Tests.Services
{
    public class EpisodeParseServiceTest
    {
        private readonly IEpisodeParseService _episodeParseService = new EpisodeParseService();

        [Fact]
        public void Parse_Episode_306213_Test()
        {            
            var sampleEpisodeRaw = SampleDataHelper.GetText(SampleDataHelper.SampleData.Episode306213);
            var episode = _episodeParseService.Parse(sampleEpisodeRaw);
            
            Assert.NotNull(episode);
            Assert.Equal((uint)306213, episode.Id);
            Assert.Equal(5, episode.GuestStars.Count);
            Assert.Equal(1, episode.Writers.Count);
            Assert.Equal(1, episode.Directors.Count);
        }
    }
}
