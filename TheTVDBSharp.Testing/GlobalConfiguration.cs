using TheTVDBSharp.Services;

namespace TheTVDBSharp.Testing
{
    public static class GlobalConfiguration
    {
        public static readonly IProxyConfiguration ApiConfiguration = 
            new ProxyConfiguration("API_KEY", "http://thetvdb.com/api");
    }
}
