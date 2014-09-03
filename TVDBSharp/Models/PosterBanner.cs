
namespace TVDBSharp.Models
{
    public sealed class PosterBanner : BannerBase
    {
        public int? Width
        {
            get;
            set;
        }

        public int? Height
        {
            get;
            set;
        }

        public PosterBanner(uint id)
            : base(id)
        {
        }
    }
}
