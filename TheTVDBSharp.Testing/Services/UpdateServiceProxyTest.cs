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
        [TestMethod]
        public async Task Retrieve_Updates_Day_Test()
        {
            var updateProvider = new UpdateServiceProxy(GlobalConfiguration.ApiConfiguration);
            var realUpdateStream = await updateProvider.Retrieve(Models.Interval.Day);

            Assert.IsNotNull(realUpdateStream);
        }
    }
}
