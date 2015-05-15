using System;

namespace TheTVDBSharp.Models
{
    public class ApiConfiguration : IApiConfiguration
    {
        public string ApiKey { get; }

        public string BaseUrl { get; }

#if WINDOWS_PORTABLE
        public TimeSpan? Timeout { get; set; }
#endif

        public ApiConfiguration(string apiKey, string baseUrl = "http://thetvdb.com")
        {
            if (baseUrl == null) throw new ArgumentNullException(nameof(baseUrl));

            // ApiKey can be null if only search is performed --> no api key required
            ApiKey = apiKey;
            BaseUrl = baseUrl;
        }
    }
}
