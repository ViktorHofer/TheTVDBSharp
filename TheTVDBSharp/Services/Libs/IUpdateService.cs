using System.IO;
using System.Threading.Tasks;
using TheTVDBSharp.Models;

namespace TheTVDBSharp.Services.Libs
{
    public interface IUpdateService
    {
        Task<Stream> Retrieve(Interval interval);

        Task<string> RetrieveUncompressed(Interval interval);
    }
}
