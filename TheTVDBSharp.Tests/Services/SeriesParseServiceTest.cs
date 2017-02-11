using System;
using System.Linq;
using System.Threading.Tasks;
using TheTVDBSharp.Models;
using TheTVDBSharp.Services;
using TheTVDBSharp.Services.Libs;
using Xunit;

namespace TheTVDBSharp.Tests.Services
{
    public class SeriesParseServiceTest
    {
        private readonly ISeriesParseService _seriesParseService;

        public SeriesParseServiceTest()
        {
            var actorParseService = new ActorParseService();
            var bannerParseService = new BannerParseService();
            var episodeParseService = new EpisodeParseService();

            _seriesParseService = new SeriesParseService(actorParseService,
                bannerParseService,
                episodeParseService); 
        }

        [Fact]
        public void Parse_Series_76156_Test()
        {
            var sampleSeriesRaw = SampleDataHelper.GetText(SampleDataHelper.SampleData.Series76156);
            var series = _seriesParseService.Parse(sampleSeriesRaw);

            Assert.NotNull(series);
            Assert.Equal((uint)76156, series.Id);
            Assert.Equal(Frequency.Wednesday, series.AirDay);
            Assert.Equal(194, series.Episodes.Count);
            Assert.Equal(1, series.Genres.Count);
            Assert.Equal(new TimeSpan(20, 0, 0), series.AirTime);
        }

        [Fact]
        public void Parse_Search_Scrubs_Test()
        {
            var sampleSeriesCollectionRaw = SampleDataHelper.GetText(SampleDataHelper.SampleData.SearchScrubs);
            var seriesCollection = _seriesParseService.ParseSearch(sampleSeriesCollectionRaw);

            Assert.NotNull(seriesCollection);
            Assert.Equal(2, seriesCollection.Count);
            Assert.Equal((uint)76156, seriesCollection.First().Id);
            Assert.Equal((uint)167151, seriesCollection.Last().Id);
        }

        [Fact]
        public async Task Parse_FullSeries_76156_Test()
        {
            var sampleFullSeriesCompressedStream = SampleDataHelper.GetStream(SampleDataHelper.SampleData.SeriesFull76156);
            var series = await _seriesParseService.ParseFull(sampleFullSeriesCompressedStream, Language.English);

            Assert.NotNull(series);
            Assert.Equal((uint)76156, series.Id);
            Assert.Equal(Frequency.Wednesday, series.AirDay);
            Assert.Equal(194, series.Episodes.Count);
            Assert.Equal(1, series.Genres.Count);
            Assert.Equal(18, series.Actors.Count);
            Assert.Equal(138, series.Banners.Count);
            Assert.Equal(new TimeSpan(20, 0, 0), series.AirTime);
        }

        [Fact]
        public void Parse_RemoveYearIfInTitle_Test()
        {
            var sampleSeriesRaw = SampleDataHelper.GetText(SampleDataHelper.SampleData.Series76156);
            var series = _seriesParseService.Parse(sampleSeriesRaw);

            Assert.Equal("Scrubs", series.Title);
        }
    }
}
