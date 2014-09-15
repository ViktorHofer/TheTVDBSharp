using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TheTVDBSharp.Models;

namespace TheTVDBSharp.Services
{
    public class BannerParseService : IBannerParseService
    {
        public IReadOnlyCollection<Banner> Parse(string bannerCollectionRaw)
        {
            var doc = XDocument.Parse(bannerCollectionRaw);
            var bannersXml = doc.Element("Banners");

            var bannerList = new List<Banner>();
            foreach (var bannerXml in bannersXml.Elements("Banner"))
            {
                var banner = Parse(bannerXml);
                bannerList.Add(banner);
            }

            return bannerList;
        }

        public Banner Parse(XElement bannerXml)
        {
            Banner banner = null;

            var id = bannerXml.ElementAsUInt("id");
            if (!id.HasValue) return null;

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
            }

            banner.Language = bannerXml.ElementAsString("Language").ToLanguage();
            banner.Rating = bannerXml.ElementAsDouble("Rating");
            banner.RatingCount = bannerXml.ElementAsInt("RatingCount");
            banner.RemotePath = bannerXml.ElementAsString("BannerPath");

            return banner;
        }

        private Banner CreateFanart(XElement bannerXml, uint id)
        {
            var banner = new FanartBanner(id);

            var size = ParseSize(bannerXml.ElementAsString("BannerType2"));
            if (size != null)
            {
                banner.Width = size.Item1;
                banner.Height = size.Item2;
            }

            var colorRawCollection = bannerXml.ElementAsString("Colors").SplitByPipe();
            if (colorRawCollection != null)
            {
                banner.Colors = colorRawCollection.Select(c =>
                {
                    var colorRawParts = c.Split(',');
                    return new Color(byte.Parse(colorRawParts[0]), byte.Parse(colorRawParts[1]), byte.Parse(colorRawParts[2]));
                }).ToArray();
            }

            banner.RemoteThumbnailPath = bannerXml.ElementAsString("ThumbnailPath");
            banner.RemoteVignettePath = bannerXml.ElementAsString("VignettePath");

            return banner;
        }

        private Banner CreatePoster(XElement bannerXml, uint id)
        {
            var banner = new PosterBanner(id);

            var size = ParseSize(bannerXml.ElementAsString("BannerType2"));
            if (size != null)
            {
                banner.Width = size.Item1;
                banner.Height = size.Item2;
            }

            return banner;
        }

        private Banner CreateSeason(XElement bannerXml, uint id)
        {
            return new SeasonBanner(id)
            {
                Season = bannerXml.ElementAsInt("Season"),
                IsWide = bannerXml.ElementAsString("BannerType2") == "seasonwide"
            };
        }

        private Banner CreateSeries(XElement bannerXml, uint id)
        {
            return new SeriesBanner(id)
            {
                BannerType = bannerXml.ElementAsEnum<SeriesBannerType>("BannerType2")
            };
        }

        private Tuple<int, int> ParseSize(string sizeRaw)
        {
            if (string.IsNullOrWhiteSpace(sizeRaw)) return null;

            var splits = sizeRaw.Split('x');
            return new Tuple<int, int>(int.Parse(splits[0]), int.Parse(splits[1]));
        }
    }
}
