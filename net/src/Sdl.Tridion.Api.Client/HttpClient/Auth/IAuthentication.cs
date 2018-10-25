using System.Net;
using Sdl.Tridion.Api.Http.Client.Request;

namespace Sdl.Tridion.Api.Http.Client.Auth
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
