using System;

namespace TheTVDBSharp.Models
{
    public class EpisodeUpdate
    {
        public uint Id { get; set; }

        public uint SeriesId { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}