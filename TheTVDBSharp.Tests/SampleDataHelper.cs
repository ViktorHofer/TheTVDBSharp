using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace TheTVDBSharp.Tests
{
    internal static class SampleDataHelper
    {
        internal enum SampleData
        {
            Episode306213,
            SearchScrubs,
            Series76156,
            SeriesFull76156,
            SeriesFull76156Meta,
            SeriesFull76156Banners,
            SeriesFull76156Actors,
            UpdatesDay
        }

        internal static async Task<string> GetTextAsync(SampleData sampleData)
        {
            var folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("SampleData");
            var file = await folder.GetFileAsync(sampleData.ToString() + ".xml");

            return await FileIO.ReadTextAsync(file);
        }

        internal static async Task<IInputStream> GetStreamAsync(SampleData sampleData)
        {
            var folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("SampleData");
            var file = await folder.GetFileAsync(sampleData.ToString() + ".zip");

            return await file.OpenReadAsync();
        }
    }
}
