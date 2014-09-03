namespace TVDBSharp.Services
{
    public class ProxyConfiguration : IProxyConfiguration
    {
        #region Properties

        public string ApiKey
        {
            get;
            private set;
        }

        public string BaseUrl
        {
            get;
            private set;
        }

        #endregion

        #region Constructor

        public ProxyConfiguration(string apiKey, string baseURL)
        {
            this.ApiKey = apiKey;
            this.BaseUrl = baseURL;
        }

        #endregion
    }
}
