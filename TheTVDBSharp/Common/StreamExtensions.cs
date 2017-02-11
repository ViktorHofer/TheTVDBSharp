using System.IO;
using System.IO.Compression;

namespace TheTVDBSharp.Common
{
    public static class StreamExtensions
    {
        public static string ReadToEnd(this ZipArchiveEntry entry)
        {
            using (var stream = entry.Open())
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
