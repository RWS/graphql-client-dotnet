using System;
using Sdl.Web.GraphQLClient.Response;
using Sdl.Web.HttpClient.Response;

namespace Sdl.Web.GraphQLClient.Exceptions
{
    public class GraphQLClientException : Exception
    {
        public int StatusCode { get; }
        public IHttpClientResponse<IGraphQLResponse> Response { get; }

        public GraphQLClientException()
        { }

        public GraphQLClientException(string msg) : base(msg)
        { }

        public GraphQLClientException(string msg, Exception ex) : base(msg, ex)
        { }

        public GraphQLClientException(IHttpClientResponse<IGraphQLResponse> response)
        {
            Response = response;
        }

        public GraphQLClientException(IHttpClientResponse<IGraphQLResponse> response, string msg) : base(msg)
        {
            Response = response;
        }

        public GraphQLClientException(IHttpClientResponse<IGraphQLResponse> response, string msg, Exception ex) : base(msg, ex)
        {
            Response = response;
        }

        public GraphQLClientException(IHttpClientResponse<IGraphQLResponse> response, string msg, Exception ex, int statusCode) : base(msg, ex)
        {
            StatusCode = statusCode;
            Response = response;
        }
    }
}
