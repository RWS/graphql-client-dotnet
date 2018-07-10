using System.Collections.Generic;

namespace Sdl.Web.GraphQLClient.Response
{
    public interface IGraphQLResponse
    {
        dynamic Data { get; set; }
        List<GraphQLError> Errors { get; set; }
    }
}
