using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using TVDBSharp.Models;
using TVDBSharp.Services;

namespace TVDBSharp.Testing.Component.Services
{
    [TestClass]
    public class SeriesParseServiceTest
    {
        private readonly ISeriesParseService seriesParseService;

        public SeriesParseServiceTest()
        {
            var actorParseService = new ActorParseService();
            var bannerParseService = new BannerParseService();
            var episodeParseService = new EpisodeParseService();

            seriesParseService = new SeriesParseService(actorParseService,
                bannerParseService,
                episodeParseService); 
        }

        [TestMethod]
        public void Parse_Series_76156_Test()
        {
            var sampleSeriesRaw = SampleDataHelper.Open(SampleDataHelper.SampleData.Series_76156);
            var series = seriesParseService.Parse(sampleSeriesRaw);

            Assert.IsNotNull(series);
            Assert.AreEqual((uint)76156, series.Id);
            Assert.AreEqual(Frequency.Wednesday, series.AirDay);
            Assert.AreEqual(194, series.Episodes.Count);
            Assert.AreEqual(1, series.Genres.Count);
            Assert.AreEqual(new TimeSpan(20, 0, 0), series.AirTime);
        }

        [TestMethod]
        public void Parse_Search_Scrubs_Test()
        {
            var sampleSeriesCollectionRaw = SampleDataHelper.Open(SampleDataHelper.SampleData.Search_Scrubs);
            var seriesCollection = seriesParseService.ParseSearch(sampleSeriesCollectionRaw);

            Assert.IsNotNull(seriesCollection);
            Assert.AreEqual(2, seriesCollection.Count);
            Assert.AreEqual((uint)76156, seriesCollection.First().Id);
            Assert.AreEqual((uint)167151, seriesCollection.Last().Id);
        }

        [TestMethod]
        public async Task Parse_FullSeries_76156_Test()
        {
            var sampleFullSeriesCompressedStream = SampleDataHelper.OpenStream(SampleDataHelper.SampleData.SeriesFull_76156);
            var series = await seriesParseService.ParseFull(sampleFullSeriesCompressedStream, Language.English);

            Assert.IsNotNull(series);
            Assert.AreEqual((uint)76156, series.Id);
            Assert.AreEqual(Frequency.Wednesday, series.AirDay);
            Assert.AreEqual(194, series.Episodes.Count);
            Assert.AreEqual(1, series.Genres.Count);
            Assert.AreEqual(18, series.Actors.Count);
            Assert.AreEqual(138, series.Banners.Count);
            Assert.AreEqual(new TimeSpan(20, 0, 0), series.AirTime);
        }

    }
}
