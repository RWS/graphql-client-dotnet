using System.Collections.Generic;

namespace Sdl.Web.GraphQL.Response
{
    public interface IGraphQLResponse
    {
        dynamic Data { get; set; }
        List<GraphQLError> Errors { get; set; }
    }
}
