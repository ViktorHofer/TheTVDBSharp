using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Xml.Linq;
using TheTVDBSharp.Models;
using TheTVDBSharp.Services;

namespace TheTVDBSharp.Testing.Providers
{
    [TestClass]
    public class SeriesServiceProxyTest
    {
        private readonly ISeriesService seriesServiceProxy = new SeriesServiceProxy(GlobalConfiguration.ApiConfiguration);

        [TestMethod]
        public async Task Search_Series_Scrubs_Test()
        {
            var seriesCollectionRaw = await this.seriesServiceProxy.Search("Scrubs", Models.Language.English);
            Assert.IsNotNull(seriesCollectionRaw);
        }

        [TestMethod]
        public async Task Retrieve_Series_Scrubs_Test()
        {
            var seriesRaw = await this.seriesServiceProxy.Retrieve(76156, Language.English);
            Assert.IsNotNull(seriesRaw);
        }

        [TestMethod]
        public async Task RetrieveFull_Series_Scrubs_Test()
        {
            var seriesRaw = await this.seriesServiceProxy.RetrieveFull(76156, Language.English);
            Assert.IsNotNull(seriesRaw);
        }
    }
}
