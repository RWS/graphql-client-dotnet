using System.Collections.Generic;
using Sdl.Tridion.Api.Http.Client;

namespace Sdl.Tridion.Api.GraphQL.Client.Response
{
    public class GraphQLTypedResponse<T> : IGraphQLTypedResponse<T>
    {
        public dynamic Data { get; set; }
        public List<GraphQLError> Errors { get; set; }
        public HttpHeaders Headers { get; set; }
        public T TypedResponseData { get; set; }
    }
}
