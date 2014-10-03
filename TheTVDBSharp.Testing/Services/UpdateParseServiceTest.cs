using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TheTVDBSharp.Services;

namespace TheTVDBSharp.Testing.Services
{
    [TestClass]
    public class UpdateParseServiceTest
    {
        private readonly IUpdateParseService _updateParseService = new UpdateParseService();

        [TestMethod]
        public void Parse_Update_Day_Test()
        {
            var updateContainerStream = SampleDataHelper.OpenStream(SampleDataHelper.SampleData.UpdatesDay);
            var updateContainer = _updateParseService.Parse(updateContainerStream, Models.Interval.Day);

            Assert.IsNotNull(updateContainer);
            Assert.AreEqual(141, updateContainer.BannerCollection.Count);
            Assert.AreEqual(3468, updateContainer.EpisodeCollection.Count);
            Assert.AreEqual(591, updateContainer.SeriesCollection.Count);
            Assert.AreEqual(new DateTime(2014, 9, 9, 17, 30, 1), updateContainer.LastUpdated);
        }
    }
}
