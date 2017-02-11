using System.Threading.Tasks;
using Xunit;

namespace TheTVDBSharp.Tests
{
    public class TheTvdbManagerTest
    {
        private readonly ITheTvdbManager _tvdbManager = GlobalConfiguration.Manager;

        [Fact]
        public async Task GetFullSeries_Test()
        {
            var series = await _tvdbManager.GetSeries(76156, Models.Language.English);
            Assert.NotNull(series);
        }

        [Fact]
        public async Task GetUpdates_Test()
        {
            var updateContainer = await _tvdbManager.GetUpdates(Models.Interval.Day);
            Assert.NotNull(updateContainer);
        }
    }
}
