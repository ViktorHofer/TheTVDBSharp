using TheTVDBSharp.Services;

namespace TheTVDBSharp.Testing
{
    public static partial class GlobalConfiguration
    {
        public static readonly string ApiKey = "";
        public static readonly string BaseUrl = "http://thetvdb.com";

        public static ProxyConfiguration ApiConfiguration
        {
            get
            {
                return new ProxyConfiguration(ApiKey, BaseUrl);
            }
        }
    }
}
