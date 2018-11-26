namespace Sdl.Tridion.Api.Http.Client.Response
{
    /// <summary>
    /// Http Client Response
    /// </summary>
    /// <typeparam name="T">Type tp use for deserialized data</typeparam>
    public class HttpClientResponse<T> : IHttpClientResponse<T>
    {
        public int StatusCode { get; set; }
        public string ContentType { get; set; }
        public HttpHeaders Headers { get; set; }
        public T ResponseData { get; set; }

        public HttpClientResponse()
        {
        }

        public HttpClientResponse(int statusCode, HttpHeaders headers, T responseData)
        {
            StatusCode = statusCode;
            Headers = headers;
            ResponseData = responseData;
        }
    }
}
