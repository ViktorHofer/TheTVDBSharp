using System;
using System.Collections.Generic;

namespace TheTVDBSharp.Models
{
    public class UpdateContainer
    {
        public DateTime LastUpdated { get; set; }

        public IReadOnlyCollection<SeriesUpdate> SeriesCollection { get; set; }

        public IReadOnlyCollection<EpisodeUpdate> EpisodeCollection { get; set; }

        public IReadOnlyCollection<BannerUpdate> BannerCollection { get; set; }
    }
}
