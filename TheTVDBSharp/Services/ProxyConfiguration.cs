namespace TheTVDBSharp.Services
{
    public class ProxyConfiguration : IProxyConfiguration
    {
        public string ApiKey { get; private set; }

        public string BaseUrl { get; private set; }

        public ProxyConfiguration(string apiKey, string baseUrl)
        {
            ApiKey = apiKey;
            BaseUrl = baseUrl;
        }
    }
}
