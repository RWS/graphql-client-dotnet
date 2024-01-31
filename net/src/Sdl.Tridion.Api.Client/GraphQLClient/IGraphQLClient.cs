using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Sdl.Tridion.Api.GraphQL.Client.Request;
using Sdl.Tridion.Api.GraphQL.Client.Response;
using Sdl.Tridion.Api.GraphQL.Client.Schema;
using Sdl.Tridion.Api.Http.Client;

namespace Sdl.Tridion.Api.GraphQL.Client
{
    /// <summary>
    /// GraphQL Client
    /// </summary>
    public interface IGraphQLClient
    {
        /// <summary>
        /// Throw exception on any GraphQL errors.
        /// </summary>
        bool ThrowOnAnyError { get; set; }

        /// <summary>
        /// Get/Sets the timeout (ms) for the requests.
        /// </summary>
        int Timeout { get; set; }

        /// <summary>
        /// Get/Sets the retry count for the requests.
        /// </summary>
        int RetryCount { get; set; }

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

        /// <summary>
        /// Returns the last errors that occured during the GraphQL request
        /// </summary>
        List<GraphQLError> LastErrors { get; }
    }
}
