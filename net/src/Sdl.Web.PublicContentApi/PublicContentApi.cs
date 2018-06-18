using System;
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
using Sdl.Web.HttpClient.Auth;

namespace Sdl.Web.PublicContentApi
{
    // TODO: Provide API + queries to support DXA
    public class PublicContentApi : IGraphQLClient, IPublicContentApi
    {
        private readonly IGraphQLClient _client;

        public PublicContentApi(Uri graphQLEndpoint)
        {
            _client = new GraphQLClient(graphQLEndpoint);
        }

        public PublicContentApi(Uri graphQLEndpoint, IAuthentication authentication)
        {
            _client = new GraphQLClient(graphQLEndpoint, authentication);
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

        public BinaryComponent GetBinaryComponent(ContentNamespace ns, int publicationId, int binaryId, IContextData contextData)
        {
            throw new NotImplementedException();
        }

        public BinaryComponent GetBinaryComponent(ContentNamespace ns, int publicationId, string url, IContextData contextData)
        {
            throw new NotImplementedException();
        }

        public List<Keyword> GetKeywords(ContentNamespace ns, int publicationId, IPagination pagination, IContextData contextData)
        {
            throw new NotImplementedException();
        }

        public Keyword GetKeyword(ContentNamespace ns, int publicationId, int categoryId, int keywordId, IContextData contextData)
        {
            throw new NotImplementedException();
        }

        public ComponentPresentation GetComponentPresentation(ContentNamespace ns, int publicationId, int componentId, int templateId, IContextData contextData)
        {
            throw new NotImplementedException();
        }

        public List<IItem> GetItems(IItemFilter itemFiter, IPagination pagination, IContextData contextData)
        {
            throw new NotImplementedException();
        }

        public Publication GetPublication(ContentNamespace ns, int publicationId, IContextData contextData)
        {
            throw new NotImplementedException();
        }

        public List<Publication> GetPublications(ContentNamespace ns, IPagination pagination,
            IPublicationFilter publicationFilter, IContextData contextData)
        {
            var contenQuery = _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = @"query publications($namespaceId: Int! $first: Int $after: String) {
                            publications(namespaceId: $namespaceId, first: $first, after: $after) {
                              edges {
                                node {
                                  id
                                  creationDate
                                  initialPublishDate
                                  itemId
                                   customMetas(filter: ""requiredMeta:publicationtitle.generated.value,FISHPRODUCTFAMILYNAME.logical.value"") {
                                    edges {
                                            node {
                                                id
                                                itemId
                                              key
                                        namespaceId
                                              publicationId
                                        value
                                              valueType
                                            }
                                        }
                                    }
                                  itemId
                                  itemType
                                  lastPublishDate
                                  namespaceId
                                  owningPublicationId
                                  publicationId
                                  title
                                  updatedDate
                                }
                            }
                        }
                      }",
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"first", pagination.First},
                    {"after", pagination.After}
                }
            });
            return contenQuery.Publications.Edges.Select(x => x.Node).ToList();
        }

        public object GetPublicationMapping(ContentNamespace ns, string uri, IContextData contextData)
        {
            throw new NotImplementedException();
        }

        public List<Page> GetPages(ContentNamespace ns, string url, IPagination pagination, IContextData contextData)
        {
            throw new NotImplementedException();
        }

        public List<StructureGroup> GetStructureGroups(ContentNamespace ns, int publicationId, int structureGroupId, IPagination pagination, IContextData contextData)
        {
            throw new NotImplementedException();
        }

        public StructureGroup GetStructureGroup(ContentNamespace ns, int publicationId, int structureGroupId, IContextData contextData)
        {
            throw new NotImplementedException();
        }
    }
}
