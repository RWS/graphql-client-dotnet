using System.Collections.Generic;
using Sdl.Tridion.Api.Http.Client;

namespace Sdl.Tridion.Api.GraphQL.Client.Response
{
    /// <summary>
    /// Represents the GraphQL respinse
    /// </summary>
    public class GraphQLResponse : IGraphQLResponse
    {
        /// <summary>
        /// GraphQL Data
        /// </summary>
        public dynamic Data { get; set; }

        /// <summary>
        /// GraphQL Errors
        /// </summary>
        public List<GraphQLError> Errors { get; set; }

        /// <summary>
        /// Response headers
        /// </summary>
        public HttpHeaders Headers { get; set; }
    }
}
