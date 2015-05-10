using System;
#if WINDOWS_PORTABLE
using System.Net.Http;
#elif WINDOWS_RUNTIME
using Windows.Web.Http;
#endif
using System.Threading.Tasks;
using TheTVDBSharp.Models;
using TheTVDBSharp.Services.Libs;

namespace TheTVDBSharp.Services
{
    public abstract class ProxyBase
    {
        protected IProxyConfiguration ProxyConfiguration { get; }

        protected ProxyBase(IProxyConfiguration proxyConfiguration)
        {
            if (proxyConfiguration == null) throw new ArgumentNullException(nameof(proxyConfiguration));
            ProxyConfiguration = proxyConfiguration;
        }

        protected async Task<HttpResponseMessage> GetAsync(string url)
        {
            if (url == null) throw new ArgumentNullException(nameof(url));

            try
            {
                var uri = new Uri(url);

                using (var client = new HttpClient())
                {
#if WINDOWS_PORTABLE
                    // Setting timeout for httpclient on portable architecture. 
                    // UAP supports timeout configuration via System.Threading.Task
                    if (ProxyConfiguration.Timeout.HasValue)
                    {
                        client.Timeout = ProxyConfiguration.Timeout.Value;
                    }
#endif

                    var response = await client.GetAsync(uri, HttpCompletionOption.ResponseContentRead);
                    if (!response.IsSuccessStatusCode) throw new BadResponseException(response.StatusCode);

                    return response;
                }
            }
            catch (Exception e)
            {
                throw new ServerNotAvailableException(inner: e);
            }
        }
    }
}
