
namespace TheTVDBSharp.Services
{
    public interface IProxyConfiguration
    {
        /// <summary>
        ///     The API key provided by TVDB.
        /// </summary>
        string ApiKey { get; }

        /// <summary>
        /// The API URL of TVDB.
        /// </summary>
        string BaseUrl { get; }
    }
}
