using System.Threading;
using System.Threading.Tasks;
using Sdl.Web.GraphQLClient.Request;
using Sdl.Web.GraphQLClient.Response;
using Sdl.Web.GraphQLClient.Schema;
using Sdl.Web.HttpClient;

namespace Sdl.Web.GraphQLClient
{
    public interface IGraphQLClient
    {
        int Timeout { get; set; }
        IHttpClient HttpClient { get; }
        IGraphQLResponse Execute(IGraphQLRequest request);
        T Execute<T>(IGraphQLRequest request);
        Task<IGraphQLResponse> ExecuteAsync(IGraphQLRequest request, CancellationToken cancellationToken);
        Task<T> ExecuteAsync<T>(IGraphQLRequest request, CancellationToken cancellationToken);
        GraphQLSchema Schema { get; }
        Task<GraphQLSchema> SchemaAsync();
    }
}
