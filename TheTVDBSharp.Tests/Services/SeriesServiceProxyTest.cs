using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using TheTVDBSharp.Models;
using TheTVDBSharp.Services;
using TheTVDBSharp.Services.Libs;

namespace TheTVDBSharp.Tests.Services
{
    [TestClass]
    public class SeriesServiceProxyTest
    {
        private readonly ISeriesService _seriesServiceProxy = new SeriesServiceProxy(GlobalConfiguration.ApiConfiguration);

        [TestMethod]
        public async Task Search_Series_Scrubs_Test()
        {
            var seriesCollectionRaw = await _seriesServiceProxy.Search("Scrubs", Language.English);
            Assert.IsNotNull(seriesCollectionRaw);
        }

        [TestMethod]
        public async Task Retrieve_Series_Scrubs_Test()
        {
            var seriesRaw = await _seriesServiceProxy.Retrieve(76156, Language.English);
            Assert.IsNotNull(seriesRaw);
        }

        [TestMethod]
        public async Task RetrieveFull_Series_Scrubs_Test()
        {
            var seriesRaw = await _seriesServiceProxy.RetrieveFull(76156, Language.English);
            Assert.IsNotNull(seriesRaw);
        }
    }
}
