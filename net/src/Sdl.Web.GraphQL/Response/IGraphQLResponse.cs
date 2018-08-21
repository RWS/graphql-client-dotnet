using System.Collections.Generic;
using Sdl.Web.HttpClient;

namespace Sdl.Web.GraphQLClient.Response
{
    /// <summary>
    /// Represents the GraphQL respinse
    /// </summary>
    public interface IGraphQLResponse
    {
        /// <summary>
        /// GraphQL Data
        /// </summary>
        dynamic Data { get; set; }

        /// <summary>
        /// GraphQL Errors
        /// </summary>
        List<GraphQLError> Errors { get; set; }

        /// <summary>
        /// Response headers
        /// </summary>
        HttpHeaders Headers { get; set; }
    }
}
