using System.Collections.Generic;

namespace Sdl.Tridion.Api.GraphQL.Client.Response
{
    /// <summary>
    /// GraphQL Error holds error information regarding the failed request.
    /// </summary>
    public class GraphQLError
    {
        /// <summary>
        /// Error message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Path to the error within the GraphQL query.
        /// </summary>
        public List<object> Path { get; set; }

        /// <summary>
        /// Locations of all errors within the GraphQL query.
        /// </summary>
        public List<GraphQLErrorLocation> Locations { get; set; }

        /// <summary>
        /// GraphQL extensions.
        /// </summary>
        public GraphQLExtensions Extensions { get; set; }
    }
}
