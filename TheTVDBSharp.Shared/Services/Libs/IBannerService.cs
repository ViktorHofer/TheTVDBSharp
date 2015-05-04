using System.Threading.Tasks;

namespace TheTVDBSharp.Services.Libs
{
    public interface IBannerService
    {
#if PORTABLE
        Task<System.IO.Stream> Retrieve(string remotePath);
#elif WINDOWS_UAP
        Task<Windows.Storage.Streams.IInputStream> Retrieve(string remotePath);
#endif
    }
}
