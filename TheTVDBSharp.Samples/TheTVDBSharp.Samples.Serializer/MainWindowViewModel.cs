using System.Collections.ObjectModel;
using TheTVDBSharp.Models;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using System.IO;
using Prism.Commands;
using Prism.Mvvm;

namespace TheTVDBSharp.Samples.Serializer
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly ITheTVDBManager Manager = GlobalConfiguration.Manager;
        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (!SetProperty(ref _searchText, value)) return;

                SearchCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand SearchCommand { get; }

        public DelegateCommand ClearCommand { get; }

        public DelegateCommand SerializeCommand { get; }

        public ObservableCollection<Series> SeriesCollection { get; } = new ObservableCollection<Series>();

        public MainWindowViewModel()
        {
            SearchCommand = new DelegateCommand(async () => await OnSearchExecuted(), () => !string.IsNullOrWhiteSpace(_searchText));
            ClearCommand = new DelegateCommand(() => SeriesCollection.Clear(), () => SeriesCollection.Count > 0);
            SerializeCommand = new DelegateCommand(OnSerializeExecuted, () => SeriesCollection.Count > 0);
            SeriesCollection.CollectionChanged += (s, e) =>
            {
                ClearCommand.RaiseCanExecuteChanged();
                SerializeCommand.RaiseCanExecuteChanged();
            };
        }

        private void OnSerializeExecuted()
        {
            var seriesCollection = SeriesCollection.ToArray();
            string seriesCollectionJson = JsonConvert.SerializeObject(seriesCollection, Formatting.Indented);
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "series.json");
            File.WriteAllText(path, seriesCollectionJson);
        }

        private async Task OnSearchExecuted()
        {
            SeriesCollection.Clear();
            var seriesCollection = await Manager.SearchSeries(_searchText, Language.English);
            foreach (var series in seriesCollection)
            {
                var completeSeries = await Manager.GetSeries(series.Id, Language.English);
                SeriesCollection.Add(completeSeries);
            }
        }
    }
}
