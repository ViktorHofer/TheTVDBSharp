
namespace TheTVDBSharp.Models
{
    public enum SeriesBannerType
    {
        Graphical,
        Text,
        Blank
    }

    public sealed class SeriesBanner : Banner
    {
        public SeriesBannerType? BannerType { get; set; }

        public SeriesBanner(uint id)
            : base(id)
        {
        }
    }
}