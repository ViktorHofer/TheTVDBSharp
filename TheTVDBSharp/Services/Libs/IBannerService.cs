using System.IO;
using System.Threading.Tasks;

namespace TheTVDBSharp.Services.Libs
{
    public interface IBannerService
    {
        Task<Stream> Retrieve(string remotePath);
    }
}