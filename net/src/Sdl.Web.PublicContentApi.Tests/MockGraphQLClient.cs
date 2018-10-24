using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sdl.Web.GraphQLClient;
using Sdl.Web.GraphQLClient.Request;
using Sdl.Web.GraphQLClient.Response;
using Sdl.Web.GraphQLClient.Schema;
using Sdl.Web.HttpClient;

namespace Sdl.Web.PublicContentApi.Tests
{
    public class MockGraphQLClient : IGraphQLClient
    {
        public bool ThrowOnAnyError { get; set; }
        public int Timeout { get; set; }
        public IHttpClient HttpClient { get; }
        public IGraphQLResponse Execute(IGraphQLRequest request)
        {
            throw new NotImplementedException();
        }

        public IGraphQLTypedResponse<T> Execute<T>(IGraphQLRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IGraphQLResponse> ExecuteAsync(IGraphQLRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IGraphQLTypedResponse<T>> ExecuteAsync<T>(IGraphQLRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public GraphQLSchema Schema { get; }
        public Task<GraphQLSchema> SchemaAsync()
        {
            throw new NotImplementedException();
        }

        public List<GraphQLError> LastErrors { get; }
    }
}
