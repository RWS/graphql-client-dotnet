using System;

namespace Sdl.Web.IQQuery.RestClient
{
    /// <summary>
    /// IRestQueryClientConfigurationProvider
    /// </summary>
    public interface IRestQueryClientConfigurationProvider
    {
        Uri Endpoint { get; }

        int Timeout { get; }

        string DefaultIndexName { get; }
    }
}
