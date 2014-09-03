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
            seriesCollectionRaw = XDocument.Parse(seriesCollectionRaw).ToString();

            var sampleSeriesCollectionRaw = SampleDataHelper.Open(SampleDataHelper.SampleData.Search_Scrubs);
            sampleSeriesCollectionRaw = XDocument.Parse(sampleSeriesCollectionRaw).ToString();

            Assert.AreEqual(sampleSeriesCollectionRaw, seriesCollectionRaw);
        }

        [TestMethod]
        public async Task Retrieve_Series_Scrubs_Test()
        {
            var seriesRaw = await this.seriesServiceProxy.Retrieve(76156, Language.English);
            seriesRaw = XDocument.Parse(seriesRaw).ToString();

            var sampleSeriesRaw = SampleDataHelper.Open(SampleDataHelper.SampleData.Series_76156);
            sampleSeriesRaw = XDocument.Parse(sampleSeriesRaw).ToString();

            Assert.AreEqual(sampleSeriesRaw, seriesRaw);
        }

        [TestMethod]
        public async Task RetrieveFull_Series_Scrubs_Test()
        {
            //var seriesProvider = new SeriesServiceProxy(GlobalConfiguration.ApiConfiguration);
            //var realSeriesTuple = await seriesProvider.RetrieveFull(76156, Models.Language.English);

            //var sampleMetaStream = SampleDataHelper.Open(SampleDataHelper.SampleData.SeriesFull_76156_Meta);
            //var sampleActorsStream = SampleDataHelper.Open(SampleDataHelper.SampleData.SeriesFull_76156_Actors);
            //var sampleBannersStream = SampleDataHelper.Open(SampleDataHelper.SampleData.SeriesFull_76156_Banners);

            //var meta = sampleMetaStream.FromXml<SeriesDataDto>();
            //var actors = sampleActorsStream.FromXml<ActorsDataDto>();
            //var banners = sampleBannersStream.FromXml<BannersDataDto>();

            //Assert.AreEqual(meta.Series.id, realSeriesTuple.Item1.Series.id);
            //Assert.AreEqual(actors.Actor.First().id, realSeriesTuple.Item2.Actor.First().id);
            //Assert.AreEqual(banners.Banner.First().id, realSeriesTuple.Item3.Banner.First().id);

            await Task.FromResult(0);
        }
    }
}
