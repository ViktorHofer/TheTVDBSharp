using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Linq;
using System.Threading.Tasks;
using TheTVDBSharp.Services;
using TheTVDBSharp.Services.Libs;

namespace TheTVDBSharp.Tests.Services
{
    [TestClass]
    public class ActorParseServiceTest
    {
        private readonly IActorParseService _actorParseService = new ActorParseService();

        [TestMethod]
        public async Task Parse_Actors_76156_Test()
        {
            var actorCollectionRaw = await SampleDataHelper.GetTextAsync(SampleDataHelper.SampleData.SeriesFull76156Actors);
            var actors = _actorParseService.Parse(actorCollectionRaw);

            Assert.IsNotNull(actors);
            Assert.AreEqual(18, actors.Count);
            Assert.AreEqual((uint)43640, actors.First().Id);
            Assert.AreEqual("Zach Braff", actors.First().Name);
            Assert.AreEqual(0, actors.First().SortOrder);
        }
    }
}
