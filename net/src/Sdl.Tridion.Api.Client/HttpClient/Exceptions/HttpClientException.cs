using System;

namespace Sdl.Tridion.Api.Http.Client.Exceptions
{
    /// <summary>
    /// Http Client Exception
    /// </summary>
    public class HttpClientException : Exception
    {
        public int StatusCode { get; }
        public string Response { get; }

        public HttpClientException()
        {
        }

        public HttpClientException(string msg) : base(msg)
        {
        }

        public HttpClientException(string msg, Exception ex) : base(msg, ex)
        {

        }

        public HttpClientException(string msg, Exception ex, int statusCode, string response) : base(msg, ex)
        {
            StatusCode = statusCode;
            Response = response;
        }
    }
}
