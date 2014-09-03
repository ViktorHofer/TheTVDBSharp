
using System.Collections.Generic;
using System.Xml.Linq;
using TVDBSharp.Models;

namespace TVDBSharp.Services
{
    public interface IBannerParseService
    {
        IReadOnlyCollection<BannerBase> Parse(string bannerCollectionRaw);

        BannerBase Parse(XElement bannerXml);
    }
}
