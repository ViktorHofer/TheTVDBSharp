
using System.Collections.Generic;
using System.Xml.Linq;
using TheTVDBSharp.Models;

namespace TheTVDBSharp.Services
{
    public interface IBannerParseService
    {
        IReadOnlyCollection<Banner> Parse(string bannerCollectionRaw);

        Banner Parse(XElement bannerXml);
    }
}
