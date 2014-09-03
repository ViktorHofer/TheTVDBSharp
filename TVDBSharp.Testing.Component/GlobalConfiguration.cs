
using TVDBSharp.Services;
namespace TVDBSharp.Testing.Component
{
    public static class GlobalConfiguration
    {
        public static readonly ITVDBManager TVDBManager;
        public static readonly IProxyConfiguration ApiConfiguration;

        static GlobalConfiguration()
        {
            ApiConfiguration = new ProxyConfiguration("B8489AFD55EF0375", "http://thetvdb.com/api");
            TVDBManager = new TVDBManager("B8489AFD55EF0375");   
        }
    }
}
