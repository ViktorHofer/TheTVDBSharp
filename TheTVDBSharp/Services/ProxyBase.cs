using System;
using System.Net.Http;
using System.Threading.Tasks;
namespace TheTVDBSharp.Services
{
    public abstract class ProxyBase
    {
        protected readonly IProxyConfiguration ProxyConfiguration;

        protected ProxyBase(IProxyConfiguration proxyConfiguration)
        {
            ProxyConfiguration = proxyConfiguration;
        }

        protected async Task<HttpResponseMessage> GetAsync(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(url, HttpCompletionOption.ResponseContentRead);
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
