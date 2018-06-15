using Sdl.Web.GraphQL.Response;
using System.Threading;
using System.Threading.Tasks;
using Sdl.Web.GraphQL.Request;
using Sdl.Web.GraphQL.Schema;

namespace Sdl.Web.GraphQL
{
    public interface IGraphQLClient
    {
        int Timeout { get; set; }
        IGraphQLResponse Execute(IGraphQLRequest request);
        T Execute<T>(IGraphQLRequest request);
        Task<IGraphQLResponse> ExecuteAsync(IGraphQLRequest request, CancellationToken cancellationToken);
        Task<T> ExecuteAsync<T>(IGraphQLRequest request, CancellationToken cancellationToken);
        GraphQLSchema Schema { get; }
        Task<GraphQLSchema> SchemaAsync();
    }
}
