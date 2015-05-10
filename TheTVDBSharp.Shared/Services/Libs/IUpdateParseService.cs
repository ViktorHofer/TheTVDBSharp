using TheTVDBSharp.Models;

namespace TheTVDBSharp.Services.Libs
{
    public interface IUpdateParseService
    {
#if WINDOWS_PORTABLE
        UpdateContainer Parse(System.IO.Stream updateContainerStream, Interval interval);
#elif WINDOWS_RUNTIME
        UpdateContainer Parse(Windows.Storage.Streams.IInputStream updateContainerStream, Interval interval);
#endif

        UpdateContainer ParseUncompressed(string updateContainerRaw);
    }
}
