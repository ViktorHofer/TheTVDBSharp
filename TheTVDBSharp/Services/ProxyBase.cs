
namespace TheTVDBSharp.Services
{
    public abstract class ProxyBase
    {
        protected readonly IProxyConfiguration proxyConfiguration;

        public ProxyBase(IProxyConfiguration proxyConfiguration)
        {
            this.proxyConfiguration = proxyConfiguration;
        }
    }
}
