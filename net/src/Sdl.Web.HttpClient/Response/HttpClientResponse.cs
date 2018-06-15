namespace Sdl.Web.HttpClient.Response
{
    /// <summary>
    /// Http Client Response
    /// </summary>
    /// <typeparam name="T">Type tp use for deserialized data</typeparam>
    public class HttpClientResponse<T> : IHttpClientResponse<T>
    {
        public int StatusCode { get; internal set; }
        public string ContentType { get; internal set; }
        public HttpHeaders Headers { get; internal set; }
        public T ResponseData { get; internal set; }

        internal HttpClientResponse()
        {
        }

        internal HttpClientResponse(int statusCode, HttpHeaders headers, T responseData)
        {
            StatusCode = statusCode;
            Headers = headers;
            ResponseData = responseData;
        }
    }
}
