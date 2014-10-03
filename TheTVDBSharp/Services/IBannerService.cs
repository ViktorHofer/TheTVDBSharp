using System.Threading.Tasks;

namespace TheTVDBSharp.Services
{
    public interface IBannerService
    {
        Task<byte[]> Retrieve(string remotePath);
    }
}
