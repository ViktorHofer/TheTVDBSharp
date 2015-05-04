using System.Threading.Tasks;
using TheTVDBSharp.Models;

namespace TheTVDBSharp.Services.Libs
{
    public interface IUpdateService
    {
#if PORTABLE
        Task<System.IO.Stream> Retrieve(Interval interval);
#elif WINDOWS_UAP
        Task<Windows.Storage.Streams.IInputStream> Retrieve(Interval interval);
#endif

        Task<string> RetrieveUncompressed(Interval interval);
    }
}
