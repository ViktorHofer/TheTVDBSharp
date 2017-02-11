using System.Threading.Tasks;
using TheTVDBSharp.Services;
using TheTVDBSharp.Services.Libs;
using Xunit;

namespace TheTVDBSharp.Tests.Services
{
    public class UpdateServiceProxyTest
    {
        readonly IUpdateService _updateService = new UpdateServiceProxy(GlobalConfiguration.ApiConfiguration);

        [Fact]
        public async Task Retrieve_Updates_Day_Test()
        {
            var realUpdateStream = await _updateService.Retrieve(Models.Interval.Day);
            Assert.NotNull(realUpdateStream);
        }
    }
}
