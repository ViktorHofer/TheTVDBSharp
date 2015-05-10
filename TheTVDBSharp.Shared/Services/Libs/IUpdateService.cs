using System.Threading.Tasks;
using TheTVDBSharp.Models;

namespace TheTVDBSharp.Services.Libs
{
    public interface IUpdateService
    {
#if WINDOWS_PORTABLE
        Task<System.IO.Stream> Retrieve(Interval interval);
#elif WINDOWS_RUNTIME
        Task<Windows.Storage.Streams.IInputStream> Retrieve(Interval interval);
#endif

        Task<string> RetrieveUncompressed(Interval interval);
    }
}
