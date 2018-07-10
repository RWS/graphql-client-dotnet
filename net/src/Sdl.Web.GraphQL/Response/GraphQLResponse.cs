using System.Collections.Generic;

namespace Sdl.Web.GraphQLClient.Response
{
    public class GraphQLResponse : IGraphQLResponse
    {
        public dynamic Data { get; set; }
        public List<GraphQLError> Errors { get; set; }
    }
}
