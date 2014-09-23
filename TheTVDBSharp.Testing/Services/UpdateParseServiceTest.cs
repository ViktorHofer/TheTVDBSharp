using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TheTVDBSharp.Services;

namespace TheTVDBSharp.Testing.Services
{
    [TestClass]
    public class UpdateParseServiceTest
    {
        private readonly IUpdateParseService updateParseService = new UpdateParseService(GlobalConfiguration.Logger);

        [TestMethod]
        public void Parse_Update_Day_Test()
        {
            var updateContainerStream = SampleDataHelper.OpenStream(SampleDataHelper.SampleData.Updates_Day);
            var updateContainer = this.updateParseService.Parse(updateContainerStream, Models.Interval.Day);

            Assert.IsNotNull(updateContainer);
            Assert.AreEqual(141, updateContainer.BannerCollection.Count);
            Assert.AreEqual(3468, updateContainer.EpisodeCollection.Count);
            Assert.AreEqual(591, updateContainer.SeriesCollection.Count);
            Assert.AreEqual(new DateTime(2014, 9, 9, 17, 30, 1), updateContainer.LastUpdated);
        }
    }
}
