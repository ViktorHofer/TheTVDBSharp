using TVDBSharp.Services;

namespace TVDBSharp.Testing.Component
{
    public static class GlobalConfiguration
    {
        public static readonly IProxyConfiguration ApiConfiguration = 
            new ProxyConfiguration("API_KEY", "http://thetvdb.com/api");
    }
}
