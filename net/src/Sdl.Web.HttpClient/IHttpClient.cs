using System;
using System.Threading;
using System.Threading.Tasks;
using Sdl.Web.HttpClient.Request;
using Sdl.Web.HttpClient.Response;

namespace Sdl.Web.HttpClient
{
    /// <summary>
    /// Http Client
    /// </summary>
    public interface IHttpClient
    {
        Uri BaseUri { get; set; }
        int Timeout { get; set; }
        string UserAgent { get; set; }
        HttpHeaders Headers { get; set; }
        IHttpClientResponse<T> Execute<T>(IHttpClientRequest request);
        Task<IHttpClientResponse<T>> ExecuteAsync<T>(IHttpClientRequest request, CancellationToken cancellationToken);
    }
}
