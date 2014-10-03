using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TheTVDBSharp.Services;

namespace TheTVDBSharp.Testing.Services
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
