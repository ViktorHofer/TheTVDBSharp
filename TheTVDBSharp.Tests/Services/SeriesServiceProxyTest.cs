using System.Threading.Tasks;
using TheTVDBSharp.Models;
using TheTVDBSharp.Services;
using TheTVDBSharp.Services.Libs;
using Xunit;

namespace TheTVDBSharp.Tests.Services
{
    public class SeriesServiceProxyTest
    {
        private readonly ISeriesService _seriesServiceProxy = new SeriesServiceProxy(GlobalConfiguration.ApiConfiguration);

        [Fact]
        public async Task Search_Series_Scrubs_Test()
        {
            var seriesCollectionRaw = await _seriesServiceProxy.Search("Scrubs", Language.English);
            Assert.NotNull(seriesCollectionRaw);
        }

        [Fact]
        public async Task Retrieve_Series_Scrubs_Test()
        {
            var seriesRaw = await _seriesServiceProxy.Retrieve(76156, Language.English);
            Assert.NotNull(seriesRaw);
        }

        [Fact]
        public async Task RetrieveFull_Series_Scrubs_Test()
        {
            var seriesRaw = await _seriesServiceProxy.RetrieveFull(76156, Language.English);
            Assert.NotNull(seriesRaw);
        }
    }
}
