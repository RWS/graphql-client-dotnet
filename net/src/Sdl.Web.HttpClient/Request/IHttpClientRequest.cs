using System;
using Sdl.Web.HttpClient.Auth;

namespace Sdl.Web.HttpClient.Request
{
    /// <summary>
    /// Http Client Request
    /// </summary>
    public interface IHttpClientRequest : ICloneable
    {
        /// <summary>
        /// Path
        /// </summary>
        string Path { get; set; }

        /// <summary>
        /// Http Method
        /// </summary>
        string Method { get; set; }

        /// <summary>
        /// Content Type
        /// </summary>
        string ContentType { get; set; }

        /// <summary>
        /// Request Body
        /// </summary>
        object Body { get; set; }

        /// <summary>
        /// Query Parameters
        /// </summary>
        HttpQueryParams QueryParameters { get; }

        /// <summary>
        /// Request Headers
        /// </summary>
        HttpHeaders Headers { get; set; }

        /// <summary>
        /// Authentication
        /// </summary>
        IAuthentication Authenticaton { get; set; }

        /// <summary>
        /// Builds full request uri
        /// </summary>
        /// <param name="httpClient">Http Client</param>
        /// <returns>Full Uri of request</returns>
        Uri BuildRequestUri(IHttpClient httpClient);
    }
}
