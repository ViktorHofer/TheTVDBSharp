using System;

namespace TheTVDBSharp.Models
{
    public class BannerUpdate
    {
        public uint SeriesId { get; set; }

        public uint? SeasonNumber { get; set; }

        public string RemotePath { get; set; }

        public Language? Language { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
