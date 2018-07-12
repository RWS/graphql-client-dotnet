using System.Collections.Generic;
using Sdl.Web.HttpClient;

namespace Sdl.Web.GraphQLClient.Response
{
    public interface IGraphQLResponse
    {
        dynamic Data { get; set; }
        List<GraphQLError> Errors { get; set; }
        HttpHeaders Headers { get; set; }
    }
}
