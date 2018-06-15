using System;
using Sdl.Web.HttpClient.Auth;
using Sdl.Web.HttpClient.Utils;

namespace Sdl.Web.HttpClient.Request
{
    /// <summary>
    /// Http Client Request
    /// </summary>
    public class HttpClientRequest : IHttpClientRequest
    {
        public string Path { get; set; }
        public string Method { get; set; } = "GET";
        public string ContentType { get; set; } = "application/json; charset=utf-8";
        public object Body { get; set; } = string.Empty;
        public HttpQueryParams QueryParameters { get; } = new HttpQueryParams();
        public HttpHeaders Headers { get; set; } = new HttpHeaders();
        public IAuthentication Authenticaton { get; set; }
        public virtual Uri BuildRequestUri(IHttpClient httpClient) => UriCreator.FromUri(httpClient.BaseUri)
            .WithPath(Path)
            .WithQueryParams(QueryParameters)
            .Build();

        public HttpClientRequest()
        {
        }

        public HttpClientRequest(IHttpClientRequest request)
        {
            Path = request.Path;
            Method = request.Method;
            ContentType = request.ContentType;
            Body = request.Body;
            QueryParameters = new HttpQueryParams(request.QueryParameters);
            Headers = new HttpHeaders(request.Headers);
            Authenticaton = request.Authenticaton;
        }

        public object Clone() => new HttpClientRequest(this);

        public override string ToString() 
            => $"HttpRequest: Path={Path} Method={Method} ContentType={ContentType}";
    }
}
