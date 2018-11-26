using System;
using System.Collections.Generic;
using System.Linq;
using Sdl.Tridion.Api.GraphQL.Client.Response;
using Sdl.Tridion.Api.Http.Client.Response;

namespace Sdl.Tridion.Api.GraphQL.Client.Exceptions
{
    /// <summary>
    /// GraphQL Client Exception
    /// </summary>
    public class GraphQLClientException : Exception
    {
        /// <summary>
        /// Http Status Code of request
        /// </summary>
        public int StatusCode { get; }

        /// <summary>
        /// GraphQL Response.
        /// </summary>
        public IGraphQLResponse GraphQLResponse { get; }

        /// <summary>
        /// Response of request.
        /// </summary>
        public IHttpClientResponse<IGraphQLResponse> Response { get; }

        public override string Message => GetMessage();

        public GraphQLClientException()
        { }

        public GraphQLClientException(string msg) : base(msg)
        { }

        public GraphQLClientException(string msg, Exception ex) : base(msg, ex)
        { }

        public GraphQLClientException(IGraphQLResponse response)
        {
            GraphQLResponse = response;
        }

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

        private string GetMessage()
        {
            var messageBuilder = new System.Text.StringBuilder();
            messageBuilder.AppendLine(base.Message);
            List<GraphQLError> errors = null;
            if (GraphQLResponse != null)
            {
                errors = GraphQLResponse.Errors;
            }
            if (Response?.ResponseData != null)
            {
                errors = Response.ResponseData.Errors;
            }
            errors?.ForEach(
                error =>
                    messageBuilder.AppendLine(
                        $"GraphQLError : {error.Message} at {string.Join(" and at ", error.Locations?.Select(loc => "Line : " + loc.Line + " Column :" + loc.Column))}"));

            return messageBuilder.ToString();
        }
    }
}
