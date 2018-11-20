namespace Sdl.Tridion.Api.Http.Client.Response
{
    /// <summary>
    /// Http Client Response
    /// </summary>
    /// <typeparam name="T">Type used to deserialize response data</typeparam>
    public interface IHttpClientResponse<out T>
    {
        /// <summary>
        /// Status Code
        /// </summary>
        int StatusCode { get; }

        /// <summary>
        /// Content Type
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// Response Headers
        /// </summary>
        HttpHeaders Headers { get; }

        /// <summary>
        /// Response Data
        /// </summary>
        T ResponseData { get; }
    }
}
