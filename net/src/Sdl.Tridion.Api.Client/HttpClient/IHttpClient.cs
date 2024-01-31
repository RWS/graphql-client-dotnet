using System;
using System.Threading;
using System.Threading.Tasks;
using Sdl.Tridion.Api.Http.Client.Request;
using Sdl.Tridion.Api.Http.Client.Response;

namespace Sdl.Tridion.Api.Http.Client
{
    /// <summary>
    /// Http Client
    /// </summary>
    public interface IHttpClient
    {
        Uri BaseUri { get; set; }
        int Timeout { get; set; }
        int RetryCount { get; set; }
        string UserAgent { get; set; }
        HttpHeaders Headers { get; set; }
        IHttpClientResponse<T> Execute<T>(IHttpClientRequest request);
        Task<IHttpClientResponse<T>> ExecuteAsync<T>(IHttpClientRequest request, CancellationToken cancellationToken);
    }
}
