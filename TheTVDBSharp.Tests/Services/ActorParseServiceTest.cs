using System.Linq;
using TheTVDBSharp.Services;
using TheTVDBSharp.Services.Libs;
using Xunit;

namespace TheTVDBSharp.Tests.Services
{
    public class ActorParseServiceTest
    {
        private readonly IActorParseService _actorParseService = new ActorParseService();

        [Fact]
        public void Parse_Actors_76156_Test()
        {
            var actorCollectionRaw = SampleDataHelper.GetText(SampleDataHelper.SampleData.SeriesFull76156Actors);
            var actors = _actorParseService.Parse(actorCollectionRaw);

            Assert.NotNull(actors);
            Assert.Equal(18, actors.Count);
            Assert.Equal((uint)43640, actors.First().Id);
            Assert.Equal("Zach Braff", actors.First().Name);
            Assert.Equal(0, actors.First().SortOrder);
        }
    }
}
