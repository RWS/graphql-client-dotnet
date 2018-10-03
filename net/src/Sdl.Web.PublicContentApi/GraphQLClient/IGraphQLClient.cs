using System.Threading;
using System.Threading.Tasks;
using Sdl.Web.GraphQLClient.Request;
using Sdl.Web.GraphQLClient.Response;
using Sdl.Web.GraphQLClient.Schema;
using Sdl.Web.HttpClient;

namespace Sdl.Web.GraphQLClient
{
    /// <summary>
    /// GraphQL Client
    /// </summary>
    public interface IGraphQLClient
    {
        /// <summary>
        /// Get/Sets the timeout (ms) for the requests.
        /// </summary>
        int Timeout { get; set; }

        /// <summary>
        /// HttpClient used for performing the actual request.
        /// </summary>
        IHttpClient HttpClient { get; }

        /// <summary>
        /// Execute a GraphQL request
        /// </summary>
        /// <param name="request">Fully built GraphQL request</param>
        /// <returns>GraphQL Response</returns>
        IGraphQLResponse Execute(IGraphQLRequest request);

        /// <summary>
        /// Execute a GraphQL request
        /// </summary>
        /// <typeparam name="T">Target Type of response data</typeparam>
        /// <param name="request">GraphQL request</param>
        /// <returns>GraphQL Response</returns>
        IGraphQLTypedResponse<T> Execute<T>(IGraphQLRequest request);

        /// <summary>
        /// Execute a GraphQL request (async)
        /// </summary>
        /// <param name="request">Fully built GraphQL request</param>
        /// <returns>GraphQL Response</returns>
        Task<IGraphQLResponse> ExecuteAsync(IGraphQLRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Execute a GraphQL request (async)
        /// </summary>
        /// <typeparam name="T">Target Type of response data</typeparam>
        /// <param name="request">GraphQL request</param>
        /// <returns>GraphQL Response</returns>
        Task<IGraphQLTypedResponse<T>> ExecuteAsync<T>(IGraphQLRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Gets GraphQL Schema from the GraphQL service.
        /// </summary>
        GraphQLSchema Schema { get; }

        /// <summary>
        /// Gets GraphQL Schema from the GraphQL service (async).
        /// </summary>
        Task<GraphQLSchema> SchemaAsync();
    }
}
