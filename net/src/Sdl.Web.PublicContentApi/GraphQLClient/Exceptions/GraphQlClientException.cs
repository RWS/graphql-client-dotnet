using System;
using System.Linq;
using Sdl.Web.GraphQLClient.Response;
using Sdl.Web.HttpClient.Response;

namespace Sdl.Web.GraphQLClient.Exceptions
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

            Response?.ResponseData?.Errors?.ForEach(error => messageBuilder.AppendLine($"GraphQLError : {error.Message} at {string.Join(" and at ", error.Locations?.Select(loc=>"Line : "+loc.Line+ " Column :" + loc.Column))}"));

            return messageBuilder.ToString();
        }
    }
}
