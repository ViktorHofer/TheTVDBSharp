using System;
using TheTVDBSharp.Services;
using TheTVDBSharp.Services.Libs;
using Xunit;

namespace TheTVDBSharp.Tests.Services
{
    public class UpdateParseServiceTest
    {
        private readonly IUpdateParseService _updateParseService = new UpdateParseService();

        [Fact]
        public void Parse_Update_Day_Test()
        {
            var updateContainerStream = SampleDataHelper.GetStream(SampleDataHelper.SampleData.UpdatesDay);
            var updateContainer = _updateParseService.Parse(updateContainerStream, Models.Interval.Day);

            Assert.NotNull(updateContainer);
            Assert.Equal(141, updateContainer.BannerCollection.Count);
            Assert.Equal(3468, updateContainer.EpisodeCollection.Count);
            Assert.Equal(591, updateContainer.SeriesCollection.Count);
            Assert.Equal(new DateTime(2014, 9, 9, 17, 30, 1), updateContainer.LastUpdated.Date.ToUniversalTime());
        }
    }
}
