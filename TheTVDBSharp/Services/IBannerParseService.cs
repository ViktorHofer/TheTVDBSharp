
using System.Collections.Generic;
using System.Xml.Linq;
using TheTVDBSharp.Models;

namespace TheTVDBSharp.Services
{
    public interface IBannerParseService
    {
        IReadOnlyCollection<BannerBase> Parse(string bannerCollectionRaw);

        BannerBase Parse(XElement bannerXml);
    }
}
