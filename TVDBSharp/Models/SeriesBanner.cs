
namespace TVDBSharp.Models
{
    public sealed class SeriesBanner : BannerBase
    {
        public SeriesBannerType? BannerType
        {
            get;
            set;
        }

        public SeriesBanner(uint id)
            : base(id)
        {
        }
    }
}