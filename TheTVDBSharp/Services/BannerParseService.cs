using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using TheTVDBSharp.Common;
using TheTVDBSharp.Models;
using TheTVDBSharp.Services.Libs;

namespace TheTVDBSharp.Services
{
    public class BannerParseService : IBannerParseService
    {
        /// <summary>
        /// Parse banner collection as string and returns null if xml is not valid
        /// </summary>
        /// <param name="bannerCollectionRaw">Banner collection xml document as string</param>
        /// <returns></returns>
        public IReadOnlyCollection<Banner> Parse(string bannerCollectionRaw)
        {
            if (bannerCollectionRaw == null) throw new ArgumentNullException(nameof(bannerCollectionRaw));

            // If xml cannot be created return null
            XDocument doc;
            try
            {
                doc = XDocument.Parse(bannerCollectionRaw);
            }
            catch (XmlException ex)
            {
                throw new ParseException("Banners collection string cannot be parsed into a xml document.", ex);
            }

            return doc.Element("Banners")?.Elements("Banner")
                .Select(Parse)
                .Where(banner => banner != null)
                .ToList() ?? throw new ParseException("Error while parsing banners xml document. Xml element 'Banners' is missing.");
        }

        /// <summary>
        /// Parse banner xml element and returns null if xml is not valid
        /// </summary>
        /// <param name="bannerXml">Banner xml element</param>
        /// <returns>Return the created banner or null if xml is not valid</returns>
        public Banner Parse(XElement bannerXml)
        {
            if (bannerXml == null) throw new ArgumentNullException(nameof(bannerXml));

            Banner banner;

            // If banner has no id return null
            var id = bannerXml.ElementAsUInt("id");
            if (!id.HasValue) throw new ParseException("Error while parsing a banner xml element. Banner id is missing.");

            var bannerType = bannerXml.ElementAsString("BannerType");
            switch (bannerType)
            {
                case "fanart":
                    banner = CreateFanart(bannerXml, id.Value);
                    break;
                case "poster":
                    banner = CreatePoster(bannerXml, id.Value);
                    break;
                case "season":
                    banner = CreateSeason(bannerXml, id.Value);
                    break;
                case "series":
                    banner = CreateSeries(bannerXml, id.Value);
                    break;
                default:
                    throw new ParseException(string.Format("Error while parsing a banner xml element. BannerType '{0}' is unknown.", bannerType));
            }

            banner.Language = bannerXml.ElementAsString("Language").ToLanguage();
            banner.Rating = bannerXml.ElementAsDouble("Rating");
            banner.RatingCount = bannerXml.ElementAsInt("RatingCount");
            banner.RemotePath = bannerXml.ElementAsString("BannerPath");

            return banner;
        }

        private static Banner CreateFanart(XElement bannerXml, uint id)
        {
            if (bannerXml == null) throw new ArgumentNullException(nameof(bannerXml));

            var banner = new FanartBanner(id);

            var size = ParseSize(bannerXml.ElementAsString("BannerType2"));
            if (size != null)
            {
                banner.Width = size.Value.width;
                banner.Height = size.Value.height;
            }

            var colorRawCollection = bannerXml.ElementAsString("Colors").SplitByPipe();
            if (colorRawCollection != null)
            {
                banner.Colors = colorRawCollection
                    .Select(c =>
                    {
                        var colorRawParts = c.Split(',');
                        return new Color
                        {
                            R = byte.Parse(colorRawParts[0]),
                            G = byte.Parse(colorRawParts[1]),
                            B = byte.Parse(colorRawParts[2])
                        };
                    })
                    .ToArray();
            }

            banner.RemoteThumbnailPath = bannerXml.ElementAsString("ThumbnailPath");
            banner.RemoteVignettePath = bannerXml.ElementAsString("VignettePath");

            return banner;
        }

        private static Banner CreatePoster(XElement bannerXml, uint id)
        {
            if (bannerXml == null) throw new ArgumentNullException(nameof(bannerXml));

            var banner = new PosterBanner(id);

            var size = ParseSize(bannerXml.ElementAsString("BannerType2"));
            if (size == null) return banner;

            banner.Width = size.Value.width;
            banner.Height = size.Value.height;

            return banner;
        }

        private static Banner CreateSeason(XElement bannerXml, uint id)
        {
            if (bannerXml == null) throw new ArgumentNullException(nameof(bannerXml));

            return new SeasonBanner(id)
            {
                Season = bannerXml.ElementAsInt("Season"),
                IsWide = bannerXml.ElementAsString("BannerType2") == "seasonwide"
            };
        }

        private static Banner CreateSeries(XElement bannerXml, uint id)
        {
            if (bannerXml == null) throw new ArgumentNullException(nameof(bannerXml));

            return new SeriesBanner(id)
            {
                BannerType = bannerXml.ElementAsEnum<SeriesBannerType>("BannerType2")
            };
        }

        public static (int width, int height)? ParseSize(string sizeRaw)
        {
            if (string.IsNullOrWhiteSpace(sizeRaw)) return null;

            var sizeRegex = new Regex(@"(\d+)x(\d+)");
            var sizeMatches = sizeRegex.Matches(sizeRaw);
            if (sizeMatches.Count != 1 || sizeMatches[0].Groups.Count != 3) return null;

            int width = int.Parse(sizeMatches[0].Groups[1].Value);
            int height = int.Parse(sizeMatches[0].Groups[2].Value);

            return (width, height);
        }
    }
}
