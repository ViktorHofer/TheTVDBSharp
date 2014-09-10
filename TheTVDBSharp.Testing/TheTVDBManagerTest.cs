using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace TheTVDBSharp.Testing
{
    [TestClass]
    public class TheTVDBManagerTest
    {
        private readonly ITheTVDBManager tvdbManager = new TheTVDBManager(GlobalConfiguration.API_KEY);

        [TestMethod]
        public async Task GetFullSeries_Test()
        {
            var series = await tvdbManager.GetSeries(76156, Models.Language.English);
            Assert.IsNotNull(series);
        }

        [TestMethod]
        public async Task GetUpdates_Test()
        {
            var updateContainer = await tvdbManager.GetUpdates(Models.Interval.Day);
            Assert.IsNotNull(updateContainer);
        }
    }
}
