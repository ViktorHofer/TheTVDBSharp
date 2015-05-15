
namespace TheTVDBSharp.Models
{
    public interface IApiConfiguration
    {
        /// <summary>
        ///     The API key provided by TVDB.
        /// </summary>
        string ApiKey { get; }

        /// <summary>
        /// The API URL of TVDB.
        /// </summary>
        string BaseUrl { get; }

#if WINDOWS_PORTABLE
        System.TimeSpan? Timeout { get; set; }
#endif
    }
}
