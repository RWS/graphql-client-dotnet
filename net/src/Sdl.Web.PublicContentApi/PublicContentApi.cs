using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sdl.Web.PublicContentApi.ContentModel;
using System.Threading;
using Newtonsoft.Json;
using Sdl.Web.GraphQLClient;
using Sdl.Web.GraphQLClient.Request;
using Sdl.Web.GraphQLClient.Response;
using Sdl.Web.GraphQLClient.Schema;
using Sdl.Web.HttpClient;
using Sdl.Web.PublicContentApi.ModelServicePlugin;
using Sdl.Web.PublicContentApi.Utils;

namespace Sdl.Web.PublicContentApi
{
    /// <summary>
    /// Public Content Api
    /// </summary>
    public class PublicContentApi : IGraphQLClient, IPublicContentApi, IPublicContentApiAsync, IModelServicePluginApi,
        IModelServicePluginApiAsync
    {
        private readonly IGraphQLClient _client;
        private readonly IModelServicePluginApi _modelserviceApi;
        private readonly IModelServicePluginApiAsync _modelserviceApiAsync;

        public PublicContentApi(IGraphQLClient graphQLclient)
        {
            _client = graphQLclient;
            _modelserviceApi = new ModelServicePluginApiImpl(_client);
            _modelserviceApiAsync = new ModelServicePluginApiImpl(_client);
        }        

        public GraphQLSchema Schema => _client.Schema;

        public async Task<GraphQLSchema> SchemaAsync() => await _client.SchemaAsync();

        #region IGraphQLClient

        public int Timeout
        {
            get { return _client.Timeout; }
            set { _client.Timeout = value; }
        }

        public IHttpClient HttpClient => _client.HttpClient;

        public IGraphQLResponse Execute(IGraphQLRequest request)
        {
            return _client.Execute(request);
        }

        public IGraphQLTypedResponse<T> Execute<T>(IGraphQLRequest request)
        {
            return _client.Execute<T>(request);
        }

        public async Task<IGraphQLResponse> ExecuteAsync(IGraphQLRequest request, CancellationToken cancellationToken)
        {
            return await _client.ExecuteAsync(request, cancellationToken);
        }

        public async Task<IGraphQLTypedResponse<T>> ExecuteAsync<T>(IGraphQLRequest request, CancellationToken cancellationToken)
        {
            return await _client.ExecuteAsync<T>(request, cancellationToken);
        }

        #endregion

        #region IPublicContentApi

        public BinaryComponent GetBinaryComponent(ContentNamespace ns, int publicationId, int binaryId,
            IContextData contextData)
        {
            return _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = InjectCustomMetaFilter(Queries.Load("BinaryComponentById", true), null),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"binaryId", binaryId},
                    {"contextData", contextData.ClaimValues}
                }
            }).TypedResponseData.BinaryComponent;
        }

        public BinaryComponent GetBinaryComponent(ContentNamespace ns, int publicationId, string url,
            IContextData contextData)
        {
            return _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = InjectCustomMetaFilter(Queries.Load("BinaryComponentByUrl", true), null),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"url", url},
                    {"contextData", contextData.ClaimValues}
                }
            }).TypedResponseData.BinaryComponent;
        }

        public BinaryComponent GetBinaryComponent(CmUri cmUri,
          IContextData contextData)
        {
            return _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = InjectCustomMetaFilter(Queries.Load("BinaryComponentByCmUri", true), null),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", cmUri.Namespace},
                    {"publicationId", cmUri.PublicationId},
                    {"cmUri", cmUri.ToString()},
                    {"contextData", contextData.ClaimValues}
                }
            }).TypedResponseData.BinaryComponent;
        }

        public ItemConnection ExecuteItemQuery(InputItemFilter filter, IPagination pagination,
            IContextData contextData, string customMetaFilter, bool renderContent)
        {
            if (contextData == null)
                contextData = new ContextData();

            // Dynamically build our item query based on the filter(s) being used.
            string query = Queries.Load("ItemQuery", false);

            // We only include the fragments that will be required based on the item types in the
            // input item filter
            if (filter.ItemTypes != null)
            {
                string fragmentList = filter.ItemTypes.Select(itemType 
                    => $"{Enum.GetName(typeof (ContentModel.ItemType), itemType).Capitialize()}Fields").Aggregate(string.Empty, (current, fragment) => current + $"...{fragment}\n");
                // Just a quick and easy way to replace markers in our queries with vars here.
                query = query.Replace("@fragmentList", fragmentList);
                query = Queries.LoadFragments(query);
            }

            query = InjectCustomMetaFilter(query, customMetaFilter);
            query = InjectRenderContentArgs(query, renderContent);
            var response = _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = query,
                Variables = new Dictionary<string, object>
                {
                    {"first", pagination.First},
                    {"after", pagination.After},
                    {"filter", filter},
                    {"contextData", contextData.ClaimValues}
                },
                Convertors = new List<JsonConverter> {new ItemConvertor()}
            });
            return response.TypedResponseData.Items;
        }

        public Publication GetPublication(ContentNamespace ns, int publicationId,
            IContextData contextData, string customMetaFilter)
        {
            if (contextData == null)
                contextData = new ContextData();
           
            var response = _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = InjectCustomMetaFilter(Queries.Load("Publication", true), customMetaFilter),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"contextData", contextData.ClaimValues}
                }
            });
            return response.TypedResponseData.Publication;
        }

        public string ResolveLink(CmUri cmUri, bool resolveToBinary = false)
        {
            var response = _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = Queries.Load("ResolveLink", true),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", cmUri.Namespace},
                    {"publicationId", cmUri.PublicationId},
                    {"type", GetLinkType(cmUri, resolveToBinary)},
                    {"itemId", cmUri.ItemId}
                }
            });
            return response.TypedResponseData.Link.Url;
        }     

        public PublicationMapping GetPublicationMapping(ContentNamespace ns, string url)
        {
            var response = _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = Queries.Load("PublicationMapping", true),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"siteUrl", url}
                }
            });
            return response.TypedResponseData.PublicationMapping;
        }

        #endregion

        #region IPublicContentApiAsync

        public async Task<BinaryComponent> GetBinaryComponentAsync(ContentNamespace ns, int publicationId, int binaryId,
            IContextData contextData, CancellationToken cancellationToken = default(CancellationToken))
        {
            return (await _client.ExecuteAsync<ContentQuery>(new GraphQLRequest
            {
                Query = Queries.Load("BinaryComponentById", true),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"binaryId", binaryId},
                    {"contextData", contextData.ClaimValues}
                }
            }, cancellationToken)).TypedResponseData.BinaryComponent;
        }       

        public async Task<BinaryComponent> GetBinaryComponentAsync(ContentNamespace ns, int publicationId, string url,
            IContextData contextData, CancellationToken cancellationToken = default(CancellationToken))
        {
            return (await _client.ExecuteAsync<ContentQuery>(new GraphQLRequest
            {
                Query = Queries.Load("BinaryComponentByUrl", true),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"url", url},
                    {"contextData", contextData.ClaimValues}
                }
            }, cancellationToken)).TypedResponseData.BinaryComponent;
        }

        public async Task<BinaryComponent> GetBinaryComponentAsync(CmUri cmUri,
            IContextData contextData, CancellationToken cancellationToken = default(CancellationToken))
        {
            return (await _client.ExecuteAsync<ContentQuery>(new GraphQLRequest
            {
                Query = Queries.Load("BinaryComponentByCmUri", true),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", cmUri.Namespace},
                    {"publicationId", cmUri.PublicationId},
                    {"cmUri", cmUri.ToString()},
                    {"contextData", contextData.ClaimValues}
                }
            }, cancellationToken)).TypedResponseData.BinaryComponent;
        }

        public async Task<ItemConnection> ExecuteItemQueryAsync(InputItemFilter filter, IPagination pagination,
            IContextData contextData, string customMetaFilter, bool renderContent,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (contextData == null)
                contextData = new ContextData();

            // Dynamically build our item query based on the filter(s) being used.
            string query = Queries.Load("ItemQuery", false);

            // We only include the fragments that will be required based on the item types in the
            // input item filter
            if (filter.ItemTypes != null)
            {
                string fragmentList = filter.ItemTypes.Select(itemType
                    => $"{Enum.GetName(typeof(ContentModel.ItemType), itemType).Capitialize()}Fields").Aggregate(string.Empty, (current, fragment) => current + $"...{fragment}\n");
                // Just a quick and easy way to replace markers in our queries with vars here.
                query = query.Replace("@fragmentList", fragmentList);
                query = Queries.LoadFragments(query);
            }

            query = InjectCustomMetaFilter(query, customMetaFilter);
            query = InjectRenderContentArgs(query, renderContent);
            var response = await _client.ExecuteAsync<ContentQuery>(new GraphQLRequest
            {
                Query = query,
                Variables = new Dictionary<string, object>
                {
                    {"first", pagination.First},
                    {"after", pagination.After},
                    {"filter", filter},
                    {"contextData", contextData.ClaimValues}
                },
                Convertors = new List<JsonConverter> { new ItemConvertor() }
            }, cancellationToken);
            return response.TypedResponseData.Items;
        }

        public async Task<Publication> GetPublicationAsync(ContentNamespace ns, int publicationId,
            IContextData contextData, string customMetaFilter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (contextData == null)
                contextData = new ContextData();

            var response = await _client.ExecuteAsync<ContentQuery>(new GraphQLRequest
            {
                Query = InjectCustomMetaFilter(Queries.Load("Publication", true), customMetaFilter),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"contextData", contextData.ClaimValues}
                }
            }, cancellationToken);
            return response.TypedResponseData.Publication;
        }

        public async Task<string> ResolveLinkAsync(CmUri cmUri, bool resolveToBinary = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _client.ExecuteAsync<ContentQuery>(new GraphQLRequest
            {
                Query = Queries.Load("ResolveLink", true),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", cmUri.Namespace},
                    {"publicationId", cmUri.PublicationId},
                    {"type", GetLinkType(cmUri, resolveToBinary)},
                    {"itemId", cmUri.ItemId}
                }
            }, cancellationToken);
            return response.TypedResponseData.Link.Url;
        }

        public async Task<PublicationMapping> GetPublicationMappingAsync(ContentNamespace ns, string url, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _client.ExecuteAsync<ContentQuery>(new GraphQLRequest
            {
                Query = Queries.Load("PublicationMapping", true),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"siteUrl", url}
                }
            }, cancellationToken);
            return response.TypedResponseData.PublicationMapping;
        }

        #endregion

        #region IModelServicePluginApi & IModelServicePluginApiAsync

        public dynamic GetPageModelData(ContentNamespace ns, int publicationId, string url, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, bool renderContent, IContextData contextData)
            =>
                _modelserviceApi.GetPageModelData(ns, publicationId, url, contentType, modelType, pageInclusion, renderContent,
                    contextData);

        public dynamic GetPageModelData(ContentNamespace ns, int publicationId, int pageId, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, bool renderContent, IContextData contextData)
            =>
                _modelserviceApi.GetPageModelData(ns, publicationId, pageId, contentType, modelType, pageInclusion, renderContent,
                    contextData);

        public dynamic GetEntityModelData(ContentNamespace ns, int publicationId, int entityId, ContentType contentType,
            DataModelType modelType, DcpType dcpType, bool renderContent, IContextData contextData)
            =>
                _modelserviceApi.GetEntityModelData(ns, publicationId, entityId, contentType, modelType, dcpType, renderContent,
                    contextData);

        public TaxonomySitemapItem GetSitemap(ContentNamespace ns, int publicationId, int descendantLevels,
            IContextData contextData)
            => _modelserviceApi.GetSitemap(ns, publicationId, descendantLevels, contextData);

        public TaxonomySitemapItem GetSitemapSubtree(ContentNamespace ns, int publicationId, string taxonomyNodeId,
            int descendantLevels,
            IContextData contextData)
            => _modelserviceApi.GetSitemapSubtree(ns, publicationId, taxonomyNodeId, descendantLevels, contextData);       

        public async Task<dynamic> GetPageModelDataAsync(ContentNamespace ns, int publicationId, string url,
            ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, bool renderContent, IContextData contextData,
            CancellationToken cancellationToken = default(CancellationToken))
            =>
                await _modelserviceApiAsync.GetPageModelDataAsync(ns, publicationId, url, contentType, modelType,
                    pageInclusion, renderContent, contextData, cancellationToken);

        public async Task<dynamic> GetPageModelDataAsync(ContentNamespace ns, int publicationId, int pageId,
            ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, bool renderContent, IContextData contextData,
            CancellationToken cancellationToken = default(CancellationToken))
            => await _modelserviceApiAsync.GetPageModelDataAsync(ns, publicationId, pageId, contentType, modelType,
                pageInclusion, renderContent, contextData, cancellationToken);

        public async Task<dynamic> GetEntityModelDataAsync(ContentNamespace ns, int publicationId, int entityId,
            ContentType contentType,
            DataModelType modelType, DcpType dcpType, bool renderContent, IContextData contextData,
            CancellationToken cancellationToken = default(CancellationToken))
            => await _modelserviceApiAsync.GetEntityModelDataAsync(ns, publicationId, entityId, contentType, modelType,
                dcpType, renderContent, contextData, cancellationToken);

        public async Task<TaxonomySitemapItem> GetSitemapAsync(ContentNamespace ns, int publicationId,
            int descendantLevels, IContextData contextData,
            CancellationToken cancellationToken = default(CancellationToken))
            =>
                await _modelserviceApiAsync.GetSitemapAsync(ns, publicationId, descendantLevels, contextData,
                    cancellationToken);

        public async Task<TaxonomySitemapItem> GetSitemapSubtreeAsync(ContentNamespace ns, int publicationId,
            string taxonomyNodeId, int descendantLevels,
            IContextData contextData, CancellationToken cancellationToken = default(CancellationToken))
            =>
                await _modelserviceApiAsync.GetSitemapSubtreeAsync(ns, publicationId, taxonomyNodeId, descendantLevels,
                    contextData, cancellationToken);

        #endregion

        #region Helpers
        protected static string InjectCustomMetaFilter(string query, string customMetaFilter)
            => query.Replace("@customMetaArgs", string.IsNullOrEmpty(customMetaFilter) ? "" : $"(filter: \"{customMetaFilter})\")");

        protected static string InjectRenderContentArgs(string query, bool renderContent)
            => query.Replace("@renderContentArgs", $"(renderContent: {(renderContent ? "true" : "false")})");

        protected static LinkType GetLinkType(CmUri cmUri, bool resolveToBinary)
        {
            if (cmUri.ItemType == ItemType.Page) return LinkType.PAGE;
            if (cmUri.ItemType == ItemType.Component && resolveToBinary) return LinkType.BINARY;
            return LinkType.COMPONENT;
        }
        #endregion
    }
}
