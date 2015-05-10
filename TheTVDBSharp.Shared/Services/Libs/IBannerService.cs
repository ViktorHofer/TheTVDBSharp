using System.Threading.Tasks;

namespace TheTVDBSharp.Services.Libs
{
    public interface IBannerService
    {
#if WINDOWS_PORTABLE
        Task<System.IO.Stream> Retrieve(string remotePath);
#elif WINDOWS_RUNTIME
        Task<Windows.Storage.Streams.IInputStream> Retrieve(string remotePath);
#endif
    }
}
