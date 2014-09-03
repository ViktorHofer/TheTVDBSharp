using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using TVDBSharp.Models;
using TVDBSharp.Models;
using TVDBSharp.Services;
using TVDBSharp.Services.Series;

namespace TVDBSharp.Services.Series
{
    public class _SeriesService : _ISeriesService
    {
        #region Fields

        private readonly ISeriesService seriesProvider;

        #endregion

        #region Constructor

        public _SeriesService(ISeriesService seriesProvider)
        {
            this.seriesProvider = seriesProvider;
        }

        #endregion

        #region ISeriesService

        public async Task<Series> RetrieveFull(int showId, Language language)
        {
            var xDoc = await this.seriesProvider.RetrieveFull(showId, language);
            //return this.Parse(xDoc.Element("Data"));
            return null;
        }

        public async Task<Series> Retrieve(int showId, Language language)
        {
            var xDoc = await this.seriesProvider.Retrieve(showId, language);
            //return this.Parse(xDoc.Element("Data"));
            return null;
        }

        public async Task<IEnumerable<Series>> Search(string query, Language language)
        {
            var data = await this.seriesProvider.Search(query, language);



            return null;
            //var desc = xDoc.Descendants("Series");
            //var seriesCollection = new List<SeriesModel>(desc.Count());

            //foreach (var element in desc)
            //{
            //    var series = this.toModel(element);
            //    seriesCollection.Add(series);
            //}

            //return seriesCollection;
        }

        public Series Parse(XElement dataElement)
        {
            return null;
        }

        #endregion
    }
}
