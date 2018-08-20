using System.Linq;
using System.Threading.Tasks;
using Sdl.Web.PublicContentApi.ContentModel;
using System.Threading;
using Sdl.Web.GraphQLClient;
using Sdl.Web.GraphQLClient.Request;
using Sdl.Web.GraphQLClient.Response;
using Sdl.Web.GraphQLClient.Schema;
using Sdl.Web.HttpClient;
using Sdl.Web.PublicContentApi.ModelServicePlugin;
using Sdl.Web.PublicContentApi.Utils;
using Sdl.Web.Core;

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
        public ILogger Logger { get; } = new NullLogger();

        public PublicContentApi(IGraphQLClient graphQLclient)
        {
            _client = graphQLclient;        
            _modelserviceApi = new ModelServicePluginApiImpl(_client, Logger);
            _modelserviceApiAsync = new ModelServicePluginApiImpl(_client, Logger);
        }

        public PublicContentApi(IGraphQLClient graphQLclient, ILogger logger)
        {
            _client = graphQLclient;
            Logger = logger ?? new NullLogger();
            _modelserviceApi = new ModelServicePluginApiImpl(_client, Logger);
            _modelserviceApiAsync = new ModelServicePluginApiImpl(_client, Logger);
        }        

        public GraphQLSchema Schema => _client.Schema;

        public async Task<GraphQLSchema> SchemaAsync() => await _client.SchemaAsync();

        #region IGraphQLClient

        public int Timeout
        {
            get { return _client.Timeout; }
            set { _client.Timeout = value; }
        }

        public IHttpClient HttpClient 
            => _client.HttpClient;

        public IGraphQLResponse Execute(IGraphQLRequest request) 
            => _client.Execute(request);

        public IGraphQLTypedResponse<T> Execute<T>(IGraphQLRequest request) 
            => _client.Execute<T>(request);

        public async Task<IGraphQLResponse> ExecuteAsync(IGraphQLRequest request, CancellationToken cancellationToken) 
            => await _client.ExecuteAsync(request, cancellationToken);

        public async Task<IGraphQLTypedResponse<T>> ExecuteAsync<T>(IGraphQLRequest request, CancellationToken cancellationToken) 
            => await _client.ExecuteAsync<T>(request, cancellationToken);

        #endregion

        #region IPublicContentApi

        public IContextData GlobalContextData { get; set; } = new ContextData();

        public BinaryComponent GetBinaryComponent(ContentNamespace ns, int publicationId, int binaryId,
            IContextData contextData) => _client.Execute<ContentQuery>(
                GraphQLRequests.GetBinaryComponent(ns, publicationId, binaryId, contextData, GlobalContextData))
                .TypedResponseData.BinaryComponent;

        public BinaryComponent GetBinaryComponent(ContentNamespace ns, int publicationId, string url,
            IContextData contextData) => _client.Execute<ContentQuery>(GraphQLRequests.GetBinaryComponent(ns, publicationId, url, contextData,
                GlobalContextData)).TypedResponseData.BinaryComponent;

        public BinaryComponent GetBinaryComponent(CmUri cmUri,
            IContextData contextData) => _client.Execute<ContentQuery>(GraphQLRequests.GetBinaryComponent(cmUri, contextData, GlobalContextData))
                .TypedResponseData.BinaryComponent;

        public ItemConnection ExecuteItemQuery(InputItemFilter filter, InputSortParam sort, IPagination pagination,
            IContextData contextData, string customMetaFilter, bool renderContent) => _client.Execute<ContentQuery>(GraphQLRequests.ExecuteItemQuery(filter, sort, pagination, contextData,
                GlobalContextData, customMetaFilter, renderContent)).TypedResponseData.Items;

        public Publication GetPublication(ContentNamespace ns, int publicationId,
            IContextData contextData, string customMetaFilter) => _client.Execute<ContentQuery>(GraphQLRequests.GetPublication(ns, publicationId, contextData,
                GlobalContextData, customMetaFilter)).TypedResponseData.Publication;

        public string ResolvePageLink(ContentNamespace ns, int publicationId, int pageId) => _client.Execute<ContentQuery>(GraphQLRequests.ResolvePageLink(ns, publicationId, pageId))
            .TypedResponseData.PageLink.Url;

        public string ResolveComponentLink(ContentNamespace ns, int publicationId, int componentId, int? sourcePageId,
            int? excludeComponentTemplateId) => _client.Execute<ContentQuery>(GraphQLRequests.ResolveComponentLink(ns, publicationId, componentId,
                sourcePageId, excludeComponentTemplateId)).TypedResponseData.ComponentLink.Url;

        public string ResolveBinaryLink(ContentNamespace ns, int publicationId, int binaryId, string variantId) => _client.Execute<ContentQuery>(GraphQLRequests.ResolveBinaryLink(ns, publicationId, binaryId, variantId))
            .TypedResponseData.BinaryLink.Url;

        public string ResolveDynamicComponentLink(ContentNamespace ns, int publicationId, int pageId, int componentId,
            int templateId) => _client.Execute<ContentQuery>(GraphQLRequests.ResolveDynamicComponentLink(ns, publicationId, pageId,
                componentId, templateId)).TypedResponseData.DynamicComponentLink.Url;

        public PublicationMapping GetPublicationMapping(ContentNamespace ns, string url) => _client.Execute<ContentQuery>(GraphQLRequests.GetPublicationMapping(ns, url))
            .TypedResponseData.PublicationMapping;

        #endregion

        #region IPublicContentApiAsync

        public async Task<BinaryComponent> GetBinaryComponentAsync(ContentNamespace ns, int publicationId, int binaryId,
            IContextData contextData, CancellationToken cancellationToken = default(CancellationToken)) => (await
                _client.ExecuteAsync<ContentQuery>(
                    GraphQLRequests.GetBinaryComponent(ns, publicationId, binaryId, contextData, GlobalContextData),
                    cancellationToken)).TypedResponseData.BinaryComponent;

        public async Task<BinaryComponent> GetBinaryComponentAsync(ContentNamespace ns, int publicationId, string url,
            IContextData contextData, CancellationToken cancellationToken = default(CancellationToken)) => (await
                _client.ExecuteAsync<ContentQuery>(
                    GraphQLRequests.GetBinaryComponent(ns, publicationId, url, contextData, GlobalContextData),
                    cancellationToken)).TypedResponseData.BinaryComponent;

        public async Task<BinaryComponent> GetBinaryComponentAsync(CmUri cmUri,
            IContextData contextData, CancellationToken cancellationToken = default(CancellationToken)) => (await
                _client.ExecuteAsync<ContentQuery>(
                    GraphQLRequests.GetBinaryComponent(cmUri, contextData, GlobalContextData), cancellationToken))
                .TypedResponseData.BinaryComponent;

        public async Task<ItemConnection> ExecuteItemQueryAsync(InputItemFilter filter, InputSortParam sort,
            IPagination pagination,
            IContextData contextData, string customMetaFilter, bool renderContent,
            CancellationToken cancellationToken = default(CancellationToken)) => (
                await
                    _client.ExecuteAsync<ContentQuery>(
                        GraphQLRequests.ExecuteItemQuery(filter, sort, pagination, contextData, GlobalContextData,
                            customMetaFilter, renderContent)
                        , cancellationToken)).TypedResponseData.Items;

        public async Task<Publication> GetPublicationAsync(ContentNamespace ns, int publicationId,
            IContextData contextData, string customMetaFilter,
            CancellationToken cancellationToken = default(CancellationToken)) => (
                await
                    _client.ExecuteAsync<ContentQuery>(
                        GraphQLRequests.GetPublication(ns, publicationId, contextData, GlobalContextData,
                            customMetaFilter), cancellationToken)).TypedResponseData.Publication;

        public async Task<string> ResolvePageLinkAsync(ContentNamespace ns, int publicationId, int pageId,
            CancellationToken cancellationToken = default(CancellationToken)) => (
                await
                    _client.ExecuteAsync<ContentQuery>(GraphQLRequests.ResolvePageLink(ns, publicationId, pageId),
                        cancellationToken)).TypedResponseData.PageLink.Url;

        public async Task<string> ResolveComponentLinkAsync(ContentNamespace ns, int publicationId, int componentId,
            int? sourcePageId,
            int? excludeComponentTemplateId, CancellationToken cancellationToken = default(CancellationToken)) => (
                await
                    _client.ExecuteAsync<ContentQuery>(
                        GraphQLRequests.ResolveComponentLink(ns, publicationId, componentId, sourcePageId,
                            excludeComponentTemplateId), cancellationToken)).TypedResponseData.ComponentLink.Url;

        public async Task<string> ResolveBinaryLinkAsync(ContentNamespace ns, int publicationId, int binaryId,
            string variantId, CancellationToken cancellationToken = default(CancellationToken)) => (
                await
                    _client.ExecuteAsync<ContentQuery>(
                        GraphQLRequests.ResolveBinaryLink(ns, publicationId, binaryId, variantId), cancellationToken))
                .TypedResponseData.BinaryLink.Url;

        public async Task<string> ResolveDynamicComponentLinkAsync(ContentNamespace ns, int publicationId, int pageId,
            int componentId,
            int templateId, CancellationToken cancellationToken = default(CancellationToken)) => (
                await
                    _client.ExecuteAsync<ContentQuery>(
                        GraphQLRequests.ResolveDynamicComponentLink(ns, publicationId, pageId, componentId, templateId),
                        cancellationToken)).TypedResponseData.DynamicComponentLink.Url;

        public async Task<PublicationMapping> GetPublicationMappingAsync(ContentNamespace ns, string url,
            CancellationToken cancellationToken = default(CancellationToken)) => (await
                _client.ExecuteAsync<ContentQuery>(GraphQLRequests.GetPublicationMapping(ns, url), cancellationToken))
                .TypedResponseData.PublicationMapping;

        #endregion

        #region IModelServicePluginApi & IModelServicePluginApiAsync

        public dynamic GetPageModelData(ContentNamespace ns, int publicationId, string url, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, bool renderContent, IContextData contextData)
            =>
                _modelserviceApi.GetPageModelData(ns, publicationId, url, contentType, modelType, pageInclusion, renderContent,
                    MergeContextData(contextData));

        public dynamic GetPageModelData(ContentNamespace ns, int publicationId, int pageId, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, bool renderContent, IContextData contextData)
            =>
                _modelserviceApi.GetPageModelData(ns, publicationId, pageId, contentType, modelType, pageInclusion, renderContent,
                    MergeContextData(contextData));

        public dynamic GetEntityModelData(ContentNamespace ns, int publicationId, int entityId, ContentType contentType,
            DataModelType modelType, DcpType dcpType, bool renderContent, IContextData contextData)
            =>
                _modelserviceApi.GetEntityModelData(ns, publicationId, entityId, contentType, modelType, dcpType, renderContent,
                    MergeContextData(contextData));

        public TaxonomySitemapItem GetSitemap(ContentNamespace ns, int publicationId, int descendantLevels,
            IContextData contextData)
            => _modelserviceApi.GetSitemap(ns, publicationId, descendantLevels, MergeContextData(contextData));

        public TaxonomySitemapItem GetSitemapSubtree(ContentNamespace ns, int publicationId, string taxonomyNodeId,
            int descendantLevels, bool includeAncestors,
            IContextData contextData)
            => _modelserviceApi.GetSitemapSubtree(ns, publicationId, taxonomyNodeId, descendantLevels, includeAncestors, MergeContextData(contextData));       

        public async Task<dynamic> GetPageModelDataAsync(ContentNamespace ns, int publicationId, string url,
            ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, bool renderContent, IContextData contextData,
            CancellationToken cancellationToken = default(CancellationToken))
            =>
                await _modelserviceApiAsync.GetPageModelDataAsync(ns, publicationId, url, contentType, modelType,
                    pageInclusion, renderContent, MergeContextData(contextData), cancellationToken);

        public async Task<dynamic> GetPageModelDataAsync(ContentNamespace ns, int publicationId, int pageId,
            ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, bool renderContent, IContextData contextData,
            CancellationToken cancellationToken = default(CancellationToken))
            => await _modelserviceApiAsync.GetPageModelDataAsync(ns, publicationId, pageId, contentType, modelType,
                pageInclusion, renderContent, MergeContextData(contextData), cancellationToken);

        public async Task<dynamic> GetEntityModelDataAsync(ContentNamespace ns, int publicationId, int entityId,
            ContentType contentType,
            DataModelType modelType, DcpType dcpType, bool renderContent, IContextData contextData,
            CancellationToken cancellationToken = default(CancellationToken))
            => await _modelserviceApiAsync.GetEntityModelDataAsync(ns, publicationId, entityId, contentType, modelType,
                dcpType, renderContent, MergeContextData(contextData), cancellationToken);

        public async Task<TaxonomySitemapItem> GetSitemapAsync(ContentNamespace ns, int publicationId,
            int descendantLevels, IContextData contextData,
            CancellationToken cancellationToken = default(CancellationToken))
            =>
                await _modelserviceApiAsync.GetSitemapAsync(ns, publicationId, descendantLevels, MergeContextData(contextData),
                    cancellationToken);

        public async Task<TaxonomySitemapItem> GetSitemapSubtreeAsync(ContentNamespace ns, int publicationId,
            string taxonomyNodeId, int descendantLevels, bool includeAncestors,
            IContextData contextData, CancellationToken cancellationToken = default(CancellationToken))
            =>
                await _modelserviceApiAsync.GetSitemapSubtreeAsync(ns, publicationId, taxonomyNodeId, descendantLevels, includeAncestors,
                    MergeContextData(contextData), cancellationToken);

        #endregion

        #region Helpers

        protected IContextData MergeContextData(IContextData localContextData)
        {
            if(localContextData == null)    
                return GlobalContextData;

            if (GlobalContextData == null)
                return localContextData;

            IContextData merged = new ContextData();
            merged.ClaimValues = GlobalContextData.ClaimValues.Concat(localContextData.ClaimValues).ToList();
            return merged;
        }
      
        #endregion
    }
}
