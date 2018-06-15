using System;
using Sdl.Web.GraphQL.Response;

namespace Sdl.Web.GraphQL.Exceptions
{
    public class GraphQLClientException : Exception
    {
        public int StatusCode { get; }
        public IGraphQLResponse Response { get; }

        public GraphQLClientException()
        {
        }

        public GraphQLClientException(string msg) : base(msg)
        {
        }

        public GraphQLClientException(string msg, Exception ex) : base(msg, ex)
        {

        }

        public GraphQLClientException(string msg, Exception ex, int statusCode, IGraphQLResponse response) : base(msg, ex)
        {
            StatusCode = statusCode;
            Response = response;
        }
    }
}
