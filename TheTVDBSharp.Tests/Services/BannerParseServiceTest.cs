using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using TheTVDBSharp.Services;
using System.Linq;
using TheTVDBSharp.Models;
using TheTVDBSharp.Services.Libs;
using System.Threading.Tasks;

namespace TheTVDBSharp.Tests.Services
{
    [TestClass]
    public class BannerParseServiceTest
    {
        private readonly IBannerParseService _bannerParseService = new BannerParseService();

        [TestMethod]
        public async Task Parse_Banners_76156_Test()
        {
            var bannerCollectionRaw = await SampleDataHelper.GetTextAsync(SampleDataHelper.SampleData.SeriesFull76156Banners);
            var banners = _bannerParseService.Parse(bannerCollectionRaw);

            Assert.IsNotNull(banners);
            Assert.AreEqual(140, banners.Count);
            Assert.AreEqual((uint)23585, banners.First().Id);
            Assert.AreEqual((byte)226, (banners.First() as FanartBanner).Colors.First().G);
        }
    }
}
