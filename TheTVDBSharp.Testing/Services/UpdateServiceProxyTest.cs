using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using TheTVDBSharp.Services;

namespace TheTVDBSharp.Testing.Services
{
    [TestClass]
    public class UpdateServiceProxyTest
    {
        IUpdateService updateService = new UpdateServiceProxy(GlobalConfiguration.ApiConfiguration);

        [TestMethod]
        public async Task Retrieve_Updates_Day_Test()
        {
            var realUpdateStream = await updateService.Retrieve(Models.Interval.Day);
            Assert.IsNotNull(realUpdateStream);
        }
    }
}
