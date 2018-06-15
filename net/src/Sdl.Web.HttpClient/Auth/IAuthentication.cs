using System.Net;
using Sdl.Web.HttpClient.Request;

namespace Sdl.Web.HttpClient.Auth
{
    /// <summary>
    /// Authentication
    /// </summary>
    public interface IAuthentication : ICredentials
    {
        /// <summary>
        /// Provide manual authentication to request (instead of server challange)
        /// </summary>
        void ApplyManualAuthentication(IHttpClientRequest request);
    }
}
