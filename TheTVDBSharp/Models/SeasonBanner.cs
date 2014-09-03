
namespace TheTVDBSharp.Models
{
    public sealed class SeasonBanner : BannerBase
    {
        public int? Season
        {
            get;
            set;
        }

        public bool? IsWide
        {
            get;
            set;
        }

        public SeasonBanner(uint id)
            : base(id)
        {
        }
    }
}
