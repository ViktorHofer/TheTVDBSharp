using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheTVDBSharp.Services;
using System.Linq;
using TheTVDBSharp.Models;

namespace TheTVDBSharp.Testing.Services
{
    [TestClass]
    public class BannerParseServiceTest
    {
        private readonly IBannerParseService bannerParseService = new BannerParseService(GlobalConfiguration.Logger);

        [TestMethod]
        public void Parse_Banners_76156_Test()
        {
            var bannerCollectionRaw = SampleDataHelper.Open(SampleDataHelper.SampleData.SeriesFull_76156_Banners);
            var banners = bannerParseService.Parse(bannerCollectionRaw);

            Assert.IsNotNull(banners);
            Assert.AreEqual(140, banners.Count);
            Assert.AreEqual((uint)23585, banners.First().Id);
            Assert.AreEqual((byte)226, (banners.First() as FanartBanner).Colors.First().G);
        }
    }
}
