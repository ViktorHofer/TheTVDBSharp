using System.IO;
using TheTVDBSharp.Models;

namespace TheTVDBSharp.Services
{
    public interface IUpdateParseService
    {
        UpdateContainer Parse(Stream updateContainerStream, Interval interval);

        UpdateContainer ParseUncompressed(string updateContainerRaw);
    }
}
