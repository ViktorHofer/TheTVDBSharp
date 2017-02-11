using System.Threading.Tasks;
using TheTVDBSharp.Services;
using TheTVDBSharp.Services.Libs;
using Xunit;

namespace TheTVDBSharp.Tests.Services
{
    public class BannerServiceProxyTest
    {
         private readonly IBannerService _bannerService = new BannerServiceProxy(GlobalConfiguration.ApiConfiguration);

         [Fact]
         public async Task Retrieve_Banner_Fanart_76156_11_Test()
         {
             var bannerStream = await _bannerService.Retrieve("fanart/original/76156-11.jpg");
             Assert.NotNull(bannerStream);
         }
    }
}
