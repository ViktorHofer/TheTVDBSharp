
namespace TheTVDBSharp.Models
{
    public sealed class PosterBanner : Banner
    {
        public int? Width { get; set; }

        public int? Height { get; set; }

        public PosterBanner(uint id)
            : base(id)
        {
        }
    }
}
