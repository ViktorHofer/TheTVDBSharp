﻿using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Threading.Tasks;
using TheTVDBSharp.Services;
using TheTVDBSharp.Services.Libs;

namespace TheTVDBSharp.Tests.Services
{
    [TestClass]
    public class EpisodeServiceProxyTest
    {
        readonly IEpisodeService _episodeService = new EpisodeServiceProxy(GlobalConfiguration.ApiConfiguration);

        [TestMethod]
        public async Task Retrieve_Episode_306213_Test()
        {
            var realEpisodeRaw = await _episodeService.Retrieve(306213, Models.Language.English);
            Assert.IsNotNull(realEpisodeRaw);
        }
    }
}