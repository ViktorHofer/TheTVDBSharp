using System;

namespace TheTVDBSharp.Models
{
    public class ApiConfiguration : IApiConfiguration
    {
        public string ApiKey { get; }

        public string BaseUrl { get; }

        public ApiConfiguration(string apiKey, string baseUrl = "http://thetvdb.com")
        {
            // ApiKey can be null if only search is performed --> no api key required
            ApiKey = apiKey;
            BaseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
        }
    }
}
