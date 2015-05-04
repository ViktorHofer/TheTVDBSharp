using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Threading.Tasks;
using TheTVDBSharp.Services;
using TheTVDBSharp.Services.Libs;

namespace TheTVDBSharp.Tests.Services
{
     [TestClass]
    public class BannerServiceProxyTest
    {
         private readonly IBannerService _bannerService = new BannerServiceProxy(GlobalConfiguration.ApiConfiguration);

         [TestMethod]
         public async Task Retrieve_Banner_Fanart_76156_11_Test()
         {
             var bannerStream = await _bannerService.Retrieve("fanart/original/76156-11.jpg");
             Assert.IsNotNull(bannerStream);
         }
    }
}
