using System;
using Sdl.Web.GraphQLClient.Response;

namespace Sdl.Web.GraphQLClient.Exceptions
{
    public class GraphQLClientException : Exception
    {
        public int StatusCode { get; }
        public IGraphQLResponse Response { get; }

        public GraphQLClientException()
        { }

        public GraphQLClientException(string msg) : base(msg)
        { }

        public GraphQLClientException(string msg, Exception ex) : base(msg, ex)
        { }

        public GraphQLClientException(IGraphQLResponse response)
        {
            Response = response;
        }

        public GraphQLClientException(IGraphQLResponse response, string msg) : base(msg)
        {
            Response = response;
        }

        public GraphQLClientException(IGraphQLResponse response, string msg, Exception ex) : base(msg, ex)
        {
            Response = response;
        }

        public GraphQLClientException(IGraphQLResponse response, string msg, Exception ex, int statusCode) : base(msg, ex)
        {
            StatusCode = statusCode;
            Response = response;
        }
    }
}
