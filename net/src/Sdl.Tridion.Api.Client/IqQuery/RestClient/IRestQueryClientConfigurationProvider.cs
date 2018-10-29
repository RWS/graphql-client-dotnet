using System;

namespace Sdl.Tridion.Api.IqQuery.RestClient
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
