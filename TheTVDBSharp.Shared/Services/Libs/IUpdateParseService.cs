using TheTVDBSharp.Models;

namespace TheTVDBSharp.Services.Libs
{
    public interface IUpdateParseService
    {
#if PORTABLE
        UpdateContainer Parse(System.IO.Stream updateContainerStream, Interval interval);
#elif WINDOWS_UAP
        UpdateContainer Parse(Windows.Storage.Streams.IInputStream updateContainerStream, Interval interval);
#endif

        UpdateContainer ParseUncompressed(string updateContainerRaw);
    }
}
