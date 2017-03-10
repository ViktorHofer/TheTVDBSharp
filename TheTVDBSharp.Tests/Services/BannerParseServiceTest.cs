using TheTVDBSharp.Services;
using System.Linq;
using TheTVDBSharp.Models;
using TheTVDBSharp.Services.Libs;
using Xunit;

namespace TheTVDBSharp.Tests.Services
{
    public class BannerParseServiceTest
    {
        private readonly IBannerParseService _bannerParseService = new BannerParseService();

        [Fact]
        public void Parse_Banners_76156_Test()
        {
            var bannerCollectionRaw = SampleDataHelper.GetText(SampleDataHelper.SampleData.SeriesFull76156Banners);
            var banners = _bannerParseService.Parse(bannerCollectionRaw);

            Assert.NotNull(banners);
            Assert.Equal(140, banners.Count);
            Assert.Equal((uint)23585, banners.First().Id);
            Assert.Equal((byte)226, (banners.First() as FanartBanner).Colors.First().G);
        }

        [Fact]
        public void ParseSize_ValidSize_Success()
        {
            var size = BannerParseService.ParseSize("320x760");

            Assert.NotNull(size);
            Assert.Equal(320, size.Value.width);
            Assert.Equal(760, size.Value.height);
        }

        [Fact]
        public void ParseSize_InvalidSize_Success()
        {
            var size = BannerParseService.ParseSize("720-360");

            Assert.Equal(size.HasValue, false);
        }
    }
}
