using System;
using System.Net.Http;
using System.Threading.Tasks;
using TheTVDBSharp.Models;

namespace TheTVDBSharp.Services
{
    public abstract class ProxyBase
    {
        protected IApiConfiguration ProxyConfiguration { get; }

        protected ProxyBase(IApiConfiguration apiConfiguration)
        {
            ProxyConfiguration = apiConfiguration ?? throw new ArgumentNullException(nameof(apiConfiguration));
        }

        protected async Task<HttpResponseMessage> GetAsync(string url)
        {
            if (url == null) throw new ArgumentNullException(nameof(url));

            try
            {
                var uri = new Uri(url);

                using (var client = new HttpClient())
                {
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
