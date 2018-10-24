using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sdl.Web.HttpClient;
using Sdl.Web.HttpClient.Request;
using Sdl.Web.HttpClient.Response;

namespace Sdl.Web.PublicContentApi.Tests
{
    public class MockHttpClient : HttpClient.HttpClient
    {
        private readonly byte[] _responseData;
        private readonly string _contentType;
        public MockHttpClient(byte[] responseData, string contentType)
        {
            _responseData = responseData;
            _contentType = contentType;
        }
        public new Uri BaseUri { get; set; }
        public new int Timeout { get; set; }
        public new string UserAgent { get; set; }
        public new HttpHeaders Headers { get; set; }
        public override IHttpClientResponse<T> Execute<T>(IHttpClientRequest request)
        {
            T deserialized = Deserialize<T>(_responseData, _contentType, request.Binder, request.Convertors);
            return new HttpClientResponse<T>
            {
                ContentType = _contentType,                
                ResponseData = deserialized
            };
        }

        public override Task<IHttpClientResponse<T>> ExecuteAsync<T>(IHttpClientRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }       
    }
}
