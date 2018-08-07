using System;
using Sdl.Web.HttpClient.Auth;

namespace Sdl.Web.IQQuery.RestClient
{
    /*
    public class RestQueryClientConfigurationProvider : IRestQueryClientConfigurationProvider
    {
        private Uri _iqService;

        public IAuthentication Authentication => new OAuth(DiscoveryServiceProvider.DefaultTokenProvider);

        public Uri Endpoint => _iqService ?? (_iqService = DiscoveryServiceProvider.Instance.ServiceClient.IQServiceUri);

        public int Timeout => AppConfigReader.GetSetting("iq-query-client-timeout", 10000);

        public string DefaultIndexName => AppConfigReader.GetSetting("iq-query-client-default-index", "udp-index");
    }*/
}
