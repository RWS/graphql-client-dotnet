using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DxaContentApiClient.GraphQL;
using Sdl.Web.PublicContentApi.ContentModel;
using Sdl.Web.GraphQL;
using Sdl.Web.GraphQL.Request;
using Sdl.Web.GraphQL.Response;
using Sdl.Web.GraphQL.Schema;
using System.Threading;

namespace Sdl.Web.PublicContentApi
{
    // TODO: Provide API + queries to support DXA
    public class PublicContentApi : IGraphQLClient
    {
        private readonly IGraphQLClient _client;

        public PublicContentApi()
        {
            _client = new GraphQLClient("http://localhost:8081/");
        }

        public GraphQLSchema Schema => _client.Schema;

        public Task<GraphQLSchema> SchemaAsync() => _client.SchemaAsync();

        public int Timeout
        {
            get { return _client.Timeout; }
            set { _client.Timeout = value; }
        }

        public IGraphQLResponse Execute(IGraphQLRequest request) 
            => _client.Execute(request);

        public T Execute<T>(IGraphQLRequest request) 
            => _client.Execute<T>(request);

        public Task<IGraphQLResponse> ExecuteAsync(IGraphQLRequest request, CancellationToken cancellationToken) 
            => _client.ExecuteAsync(request, cancellationToken);

        public Task<T> ExecuteAsync<T>(IGraphQLRequest request, CancellationToken cancellationToken) 
            => _client.ExecuteAsync<T>(request, cancellationToken);

        public List<Publication> Publications()
        {
            // example
            var contenQuery = Execute<ContentQuery>(new GraphQLRequest
            {
                Query = @"
                        {
                          publications(namespaceId: 2, first: 5) {
                            edges {
                              cursor
                              node {
                               lastPublishDate
                                id
                                itemId
                                itemType
                                updatedDate
                                customMetas(filter: ""requiredMeta:publicationtitle.generated.value,FISHPRODUCTFAMILYNAME.logical.value"") {
                                  edges {
                                        node {
                                            key
                                            value
                                          valueType
                                        }
                                    }
                                }
                            }
                        }
                      }
                    }
                "
            });

            return contenQuery.Publications.Edges.Select(x => x.Node).ToList();
        }        
    }
}
