using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using TheTVDBSharp.Models;

namespace TheTVDBSharp.Services
{
    public class UpdateParseService : IUpdateParseService
    {
        private readonly ISimpleLogger logger;

        public UpdateParseService(ISimpleLogger logger)
        {
            this.logger = logger;
        }

        public UpdateContainer Parse(Stream updateContainerStream, Interval interval)
        {
            using (ZipArchive archive = new ZipArchive(updateContainerStream, ZipArchiveMode.Read))
            {
                var entryName = string.Format("updates_{0}.xml", interval.ToApiString());
                var updateContainerRaw = archive.GetEntry(entryName).ReadToEnd();
                return ParseUncompressed(updateContainerRaw);
            }
        }

        public UpdateContainer ParseUncompressed(string updateContainerRaw)
        {
            if (string.IsNullOrWhiteSpace(updateContainerRaw)) throw new ArgumentNullException("updateContainerRaw", "Update container xml document as string cannot be null");

            // If xml cannot be created return null
            XDocument doc;
            try
            {
                doc = XDocument.Parse(updateContainerRaw);
            }
            catch (XmlException e)
            {
                this.logger.Log("Search series collection string cannot be parsed into a xml document.", LogLevel.Error, e);
                return null;
            }

            var updateContainerXml = doc.Element("Data");
            if (updateContainerXml == null)
            {
                this.logger.Log("Error while parsing update xml document. Xml Element 'Data' is missing.", LogLevel.Error);
                return null;
            }

            UpdateContainer updateContainer = new UpdateContainer();

            uint lastUpdatedEpoch;
            var lastUpdatedRaw = updateContainerXml.Attribute("time").Value;
            if (lastUpdatedRaw != null && uint.TryParse(lastUpdatedRaw, out lastUpdatedEpoch))
            {
                updateContainer.LastUpdated = lastUpdatedEpoch.ToDateTime();
            }

            updateContainer.SeriesCollection = updateContainerXml.Elements("Series")
                .Select(seriesUpdateXml => ParseSeriesUpdate(seriesUpdateXml))
                .ToList();

            updateContainer.EpisodeCollection = updateContainerXml.Elements("Episode")
                .Select(episodeUpdateXml => ParseEpisodeUpdate(episodeUpdateXml))
                .ToList();

            updateContainer.BannerCollection = updateContainerXml.Elements("Banner")
                .Select(bannerUpdateXml => ParseBannerUpdate(bannerUpdateXml))
                .ToList();

            return updateContainer;
        }

        private SeriesUpdate ParseSeriesUpdate(XElement seriesUpdateXml)
        {
            return new SeriesUpdate()
            {
                Id = seriesUpdateXml.ElementAsUInt("id").Value,
                LastUpdated = seriesUpdateXml.ElementFromEpochToDateTime("time").Value
            };
        }

        private EpisodeUpdate ParseEpisodeUpdate(XElement episodeUpdateXml)
        {
            return new EpisodeUpdate()
            {
                Id = episodeUpdateXml.ElementAsUInt("id").Value,
                SeriesId = episodeUpdateXml.ElementAsUInt("Series").Value,
                LastUpdated = episodeUpdateXml.ElementFromEpochToDateTime("time").Value
            };
        }

        private BannerUpdate ParseBannerUpdate(XElement bannerUpdateXml)
        {
            return new BannerUpdate()
            {
                SeriesId = bannerUpdateXml.ElementAsUInt("Series").Value,
                RemotePath = bannerUpdateXml.ElementAsString("path"),
                SeasonNumber = bannerUpdateXml.ElementAsUInt("SeasonNum"),
                Language = bannerUpdateXml.ElementAsString("language").ToLanguage(),
                LastUpdated = bannerUpdateXml.ElementFromEpochToDateTime("time").Value
            };
        }
    }
}
