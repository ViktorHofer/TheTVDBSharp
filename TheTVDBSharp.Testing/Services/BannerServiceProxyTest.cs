using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TheTVDBSharp.Services;

namespace TheTVDBSharp.Testing.Services
{
     [TestClass]
    public class BannerServiceProxyTest
    {
         private readonly IBannerService bannerService = new BannerServiceProxy(GlobalConfiguration.ApiConfiguration);

         [TestMethod]
         public async Task Retrieve_Banner_Fanart_76156_11_Test()
         {
             var bannerBytes = await bannerService.Retrieve("fanart/original/76156-11.jpg");
             Assert.IsNotNull(bannerBytes);
         }
    }
}
