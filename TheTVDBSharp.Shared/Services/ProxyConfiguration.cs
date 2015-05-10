using System;
using TheTVDBSharp.Services.Libs;

namespace TheTVDBSharp.Services
{
    public class ProxyConfiguration : IProxyConfiguration
    {
        public string ApiKey { get; }

        public string BaseUrl { get; }

#if WINDOWS_PORTABLE
        public TimeSpan? Timeout { get; set; }
#endif

        public ProxyConfiguration(string apiKey, string baseUrl)
        {
            if (apiKey == null) throw new ArgumentNullException(nameof(apiKey));
            if (baseUrl == null) throw new ArgumentNullException(nameof(baseUrl));

            ApiKey = apiKey;
            BaseUrl = baseUrl;
        }
    }
}
