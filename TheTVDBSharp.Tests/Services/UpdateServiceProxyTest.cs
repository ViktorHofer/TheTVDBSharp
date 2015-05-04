using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Threading.Tasks;
using TheTVDBSharp.Services;
using TheTVDBSharp.Services.Libs;

namespace TheTVDBSharp.Tests.Services
{
    [TestClass]
    public class UpdateServiceProxyTest
    {
        readonly IUpdateService _updateService = new UpdateServiceProxy(GlobalConfiguration.ApiConfiguration);

        [TestMethod]
        public async Task Retrieve_Updates_Day_Test()
        {
            var realUpdateStream = await _updateService.Retrieve(Models.Interval.Day);
            Assert.IsNotNull(realUpdateStream);
        }
    }
}
