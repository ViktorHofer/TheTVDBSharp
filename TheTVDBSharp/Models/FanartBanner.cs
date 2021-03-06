﻿using System.Collections.Generic;
#if WINDOWS_RUNTIME
using Windows.UI;
#endif

namespace TheTVDBSharp.Models
{
    public sealed class FanartBanner : Banner
    {
        public int? Width { get; set; }

        public int? Height { get; set; }

        public IReadOnlyCollection<Color> Colors { get; set; }

        public string RemoteThumbnailPath { get; set; }

        public string RemoteVignettePath { get; set; }

        public FanartBanner(uint id)
            : base(id)
        {
        }
    }
}
