using Microsoft.VisualStudio.TestTools.UnitTesting;
using TVDBSharp.Services;
using System.Linq;
using TVDBSharp.Models;

namespace TVDBSharp.Testing.Component.Services
{
    [TestClass]
    public class BannerParseServiceTest
    {
        private readonly IBannerParseService bannerParseService = new BannerParseService();

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
