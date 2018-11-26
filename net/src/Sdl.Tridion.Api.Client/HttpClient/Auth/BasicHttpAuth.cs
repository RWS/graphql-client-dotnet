using System;
using System.Net;
using System.Text;
using Sdl.Tridion.Api.Http.Client.Request;

namespace Sdl.Tridion.Api.Http.Client.Auth
{
    public class BasicHttpAuth : IAuthentication
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public ICredentials Credentials { get; } = null;

        public void ApplyManualAuthentication(IHttpClientRequest request)
        {
            // manually add authentication to request
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                return;
            // add basic http auth to request headers
            request.Headers.Add("Authorization", 
                $"Basic {Convert.ToBase64String(Encoding.Default.GetBytes($"{Username}:{Password}"))}");
        }
       
        public NetworkCredential GetCredential(Uri uri, string authType) => null;
    }
}
