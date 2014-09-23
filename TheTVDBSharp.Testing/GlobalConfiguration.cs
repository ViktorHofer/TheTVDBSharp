using TheTVDBSharp.Services;

namespace TheTVDBSharp.Testing
{
    public static partial class GlobalConfiguration
    {
        public static readonly ISimpleLogger Logger = new SimpleLogger();
        public static readonly string API_KEY = "";
        public static readonly string BASE_URL = "http://thetvdb.com";

        public static ProxyConfiguration ApiConfiguration
        {
            get
            {
                return new ProxyConfiguration(API_KEY, BASE_URL);
            }
        }
    }
}
