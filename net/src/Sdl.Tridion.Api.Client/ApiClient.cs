using System.Collections.Generic;
using System.Threading.Tasks;
using Sdl.Tridion.Api.Client.ContentModel;
using System.Threading;
using Microsoft.CSharp.RuntimeBinder;
using Sdl.Tridion.Api.GraphQL.Client;
using Sdl.Tridion.Api.GraphQL.Client.Request;
using Sdl.Tridion.Api.GraphQL.Client.Response;
using Sdl.Tridion.Api.GraphQL.Client.Schema;
using Sdl.Tridion.Api.Http.Client;
using Sdl.Tridion.Api.Client.Utils;
using Sdl.Tridion.Api.Client.Core;
using Sdl.Tridion.Api.Client.Exceptions;

namespace Sdl.Tridion.Api.Client
{
    /// <summary>
    /// Content Api
    /// </summary>
    public class ApiClient : IGraphQLClient, IApiClient, IApiClientAsync
    {
        private readonly IGraphQLClient _client;
        public ILogger Logger { get; } = new NullLogger();

        public ApiClient(IGraphQLClient graphQLclient)
        {
            _client = graphQLclient;
        }

        public ApiClient(IGraphQLClient graphQLclient, ILogger logger)
        {
            _client = graphQLclient;
            Logger = logger ?? new NullLogger();
        }

        #region IGraphQLClient

        public GraphQLSchema Schema => _client.Schema;

        public async Task<GraphQLSchema> SchemaAsync() => await _client.SchemaAsync();

        public bool HasErrors => LastErrors != null && LastErrors.Count > 0;

        public List<GraphQLError> LastErrors => _client.LastErrors;

        public bool ThrowOnAnyError
        {
            get { return _client.ThrowOnAnyError; }
            set { _client.ThrowOnAnyError = value; }
        }

        public int Timeout
        {
            get { return _client.Timeout; }
            set { _client.Timeout = value; }
        }

        public int RetryCount
        {
            get { return _client.RetryCount; }
            set { _client.RetryCount = value; }
        }

        public IHttpClient HttpClient
            => _client.HttpClient;

        public IGraphQLResponse Execute(IGraphQLRequest request)
            => _client.Execute(request);

        public IGraphQLTypedResponse<T> Execute<T>(IGraphQLRequest request)
            => _client.Execute<T>(request);

        public Task<IGraphQLResponse> ExecuteAsync(IGraphQLRequest request, CancellationToken cancellationToken)
            => _client.ExecuteAsync(request, cancellationToken);

        public Task<IGraphQLTypedResponse<T>> ExecuteAsync<T>(IGraphQLRequest request,
            CancellationToken cancellationToken)
            => _client.ExecuteAsync<T>(request, cancellationToken);

        #endregion

        #region IApiClient

        /// <summary>
        /// Holds global context data passed on to PCA service. Note that context data passed
        /// directly to API methods overwrites these values.
        /// </summary>
        public IContextData GlobalContextData { get; set; } = new ContextData();

        /// <summary>
        /// Specify type of content to return from API. When set to RAW no conversion will take
        /// place otherwise its treated as model data and will go through conversion to type specified
        /// by DefaultModelType (default: Model)
        /// </summary>
        public ContentType DefaultContentType { get; set; } = ContentType.MODEL;

        /// <summary>
        /// Specify model type to return (default: R2)
        /// </summary>
        public DataModelType DefaultModelType { get; set; } = DataModelType.R2;

        /// <summary>
        /// Specify how tcdl links get rendered (default: relative)
        /// </summary>
        public TcdlLinkRendering TcdlLinkRenderingType { get; set; } = TcdlLinkRendering.Relative;

        /// <summary>
        /// Specify how the model-service plugin renders links (default: relative)
        /// </summary>
        public ModelServiceLinkRendering ModelSericeLinkRenderingType { get; set; } = ModelServiceLinkRendering.Relative;

        /// <summary>
        /// Specify Url prefix for tcdl links for Absolute rendering type (default: none)
        /// </summary>
        public string TcdlLinkUrlPrefix { get; set; } = null;

        /// <summary>
        /// Specify Url prefix for tcdl binary links for Absolute rendering type (default: none)
        /// </summary>
        public string TcdlBinaryLinkUrlPrefix { get; set; } = null;

        public ComponentPresentation GetComponentPresentation(ContentNamespace ns, int publicationId, int componentId,
            int templateId,
            string customMetaFilter, ContentIncludeMode contentIncludeMode, IContextData contextData)
            => _client.Execute<ContentQuery>(
                GraphQLRequests.ComponentPresentation(ns, publicationId, componentId, templateId, customMetaFilter,
                    contentIncludeMode, contextData, GlobalContextDataInternal))
                .TypedResponseData.ComponentPresentation;

        public ComponentPresentationConnection GetComponentPresentations(ContentNamespace ns, int publicationId,
            InputComponentPresentationFilter filter, InputSortParam sort, IPagination pagination,
            string customMetaFilter,
            ContentIncludeMode contentIncludeMode, IContextData contextData)
            => _client.Execute<ContentQuery>(
                GraphQLRequests.ComponentPresentations(ns, publicationId, filter, sort, pagination, customMetaFilter,
                    contentIncludeMode, contextData, GlobalContextDataInternal))
                .TypedResponseData.ComponentPresentations;

        public Page GetPage(ContentNamespace ns, int publicationId, int pageId, string customMetaFilter,
            ContentIncludeMode contentIncludeMode,
            IContextData contextData)
            => _client.Execute<ContentQuery>(
                GraphQLRequests.Page(ns, publicationId, pageId, customMetaFilter, contentIncludeMode, contextData,
                    GlobalContextDataInternal))
                .TypedResponseData.Page;

        public Page GetPage(ContentNamespace ns, int publicationId, string url, string customMetaFilter,
            ContentIncludeMode contentIncludeMode,
            IContextData contextData)
            => _client.Execute<ContentQuery>(
                GraphQLRequests.Page(ns, publicationId, url, customMetaFilter, contentIncludeMode, contextData,
                    GlobalContextDataInternal))
                .TypedResponseData.Page;

        public Page GetPage(CmUri cmUri, string customMetaFilter, ContentIncludeMode contentIncludeMode,
            IContextData contextData)
            => _client.Execute<ContentQuery>(
                GraphQLRequests.Page(cmUri, customMetaFilter, contentIncludeMode, contextData, GlobalContextDataInternal))
                .TypedResponseData.Page;

        public PageConnection GetPages(ContentNamespace ns, IPagination pagination, string url,
            string customMetaFilter, ContentIncludeMode contentIncludeMode, IContextData contextData)
            => _client.Execute<ContentQuery>(
                GraphQLRequests.Pages(ns, pagination, url, customMetaFilter, contentIncludeMode, contextData,
                    GlobalContextDataInternal))
                .TypedResponseData.Pages;

        public BinaryComponent GetBinaryComponent(ContentNamespace ns, int publicationId, int binaryId,
            string customMetaFilter,
            IContextData contextData) => _client.Execute<ContentQuery>(
                GraphQLRequests.BinaryComponent(ns, publicationId, binaryId, customMetaFilter, contextData,
                    GlobalContextDataInternal))
                .TypedResponseData.BinaryComponent;

        public BinaryComponent GetBinaryComponent(ContentNamespace ns, int publicationId, string url,
            string customMetaFilter,
            IContextData contextData)
            =>
                _client.Execute<ContentQuery>(GraphQLRequests.BinaryComponent(ns, publicationId, url, customMetaFilter,
                    contextData,
                    GlobalContextDataInternal)).TypedResponseData.BinaryComponent;

        public BinaryComponent GetBinaryComponent(CmUri cmUri, string customMetaFilter, IContextData contextData)
            =>
                _client.Execute<ContentQuery>(GraphQLRequests.BinaryComponent(cmUri, customMetaFilter, contextData,
                    GlobalContextDataInternal))
                    .TypedResponseData.BinaryComponent;

        public ItemConnection ExecuteItemQuery(InputItemFilter filter, InputSortParam sort, IPagination pagination,
            string customMetaFilter, ContentIncludeMode contentIncludeMode, bool includeContainerItems,
            IContextData contextData)
            =>
                _client.Execute<ContentQuery>(GraphQLRequests.ExecuteItemQuery(filter, sort, pagination,
                    customMetaFilter, contentIncludeMode, includeContainerItems,
                    contextData, GlobalContextDataInternal)).TypedResponseData.Items;

        public T ExecuteExternalItemQuery<T>(string eclUri, string itemType, List<string> itemFields)
        => _client.Execute<ExternalItemConnection<T>>(GraphQLRequests.BuildExternalItemQuery(eclUri, itemType, itemFields)).TypedResponseData.ExternalItem;

        public Publication GetPublication(ContentNamespace ns, int publicationId,
            string customMetaFilter, IContextData contextData)
            => _client.Execute<ContentQuery>(GraphQLRequests.Publication(ns, publicationId, customMetaFilter,
                contextData, GlobalContextDataInternal)).TypedResponseData.Publication;

        public PublicationConnection GetPublications(ContentNamespace ns, IPagination pagination,
            InputPublicationFilter filter, string customMetaFilter,
            IContextData contextData)
            =>
                _client.Execute<ContentQuery>(GraphQLRequests.Publications(ns, pagination, filter, customMetaFilter,
                    contextData,
                    GlobalContextDataInternal)).TypedResponseData.Publications;

        public string ResolvePageLink(ContentNamespace ns, int publicationId, int pageId, bool renderRelativeLink = true)
            =>
                _client.Execute<ContentQuery>(GraphQLRequests.ResolvePageLink(ns, publicationId, pageId,
                    renderRelativeLink))
                    .TypedResponseData.PageLink.Url;

        public string ResolveComponentLink(ContentNamespace ns, int publicationId, int componentId, int? sourcePageId,
            int? excludeComponentTemplateId, bool renderRelativeLink = true)
            => _client.Execute<ContentQuery>(GraphQLRequests.ResolveComponentLink(ns, publicationId, componentId,
                sourcePageId, excludeComponentTemplateId, renderRelativeLink)).TypedResponseData.ComponentLink.Url;

        public string ResolveBinaryLink(ContentNamespace ns, int publicationId, int binaryId, string variantId,
            bool renderRelativeLink = true)
            =>
                _client.Execute<ContentQuery>(GraphQLRequests.ResolveBinaryLink(ns, publicationId, binaryId, variantId,
                    renderRelativeLink))
                    .TypedResponseData.BinaryLink.Url;

        public string ResolveDynamicComponentLink(ContentNamespace ns, int publicationId, int pageId, int componentId,
            int templateId, bool renderRelativeLink = true)
            => _client.Execute<ContentQuery>(GraphQLRequests.ResolveDynamicComponentLink(ns, publicationId, pageId,
                componentId, templateId, renderRelativeLink)).TypedResponseData.DynamicComponentLink.Url;

        public PublicationMapping GetPublicationMapping(ContentNamespace ns, string url)
            => _client.Execute<ContentQuery>(GraphQLRequests.PublicationMapping(ns, url))
                .TypedResponseData.PublicationMapping;

        public dynamic GetPageModelData(ContentNamespace ns, int publicationId, int pageId,
            PageInclusion pageInclusion, ContentIncludeMode contentIncludeMode, IContextData contextData)
        {
            try
            {
                var response =
                    _client.Execute(GraphQLRequests.PageModelData(ns, publicationId, pageId,
                        pageInclusion, contentIncludeMode, contextData, GlobalContextDataInternal));
                return response.Data.page.rawContent.data;
            }
            catch (RuntimeBinderException e)
            {
                throw new ApiException(
                    $"Failed to get page model data (namespaceId:{ns}, publicationId:{publicationId}, pageId:{pageId}",
                    e);
            }
        }

        public dynamic GetPageModelData(ContentNamespace ns, int publicationId, string url,
            PageInclusion pageInclusion, ContentIncludeMode contentIncludeMode, IContextData contextData)
        {
            try
            {
                var response =
                    _client.Execute(GraphQLRequests.PageModelData(ns, publicationId, url,
                        pageInclusion, contentIncludeMode, contextData, GlobalContextDataInternal));
                return response.Data.page.rawContent.data;
            }
            catch (RuntimeBinderException e)
            {
                throw new ApiException(
                    $"Failed to get page model data (namespaceId:{ns}, publicationId:{publicationId}, url:{url}", e);
            }
        }

        public dynamic GetEntityModelData(ContentNamespace ns, int publicationId, int entityId, int templateId,
            ContentIncludeMode contentIncludeMode, IContextData contextData)
        {
            try
            {
                var response =
                    _client.Execute(GraphQLRequests.EntityModelData(ns, publicationId, entityId, templateId,
                        contentIncludeMode, contextData, GlobalContextDataInternal));
                return response.Data.componentPresentation.rawContent.data;
            }
            catch (RuntimeBinderException e)
            {
                throw new ApiException(
                    $"Failed to get enity model data (namespaceId:{ns}, publicationId:{publicationId}, entityId:{entityId}",
                    e);
            }
        }

        public TaxonomySitemapItem GetSitemap(ContentNamespace ns, int publicationId, int descendantLevels,
            IContextData contextData)
        {
            try
            {
                var response =
                    _client.Execute<ContentQuery>(GraphQLRequests.Sitemap(ns, publicationId, descendantLevels,
                        contextData, GlobalContextDataInternal));
                return response.TypedResponseData.Sitemap;
            }
            catch (RuntimeBinderException e)
            {
                throw new ApiException(
                    $"Failed to get sitemap (namespaceId:{ns}, publicationId:{publicationId}, descendantLevels:{descendantLevels}",
                    e);
            }
        }

        public List<TaxonomySitemapItem> GetSitemapSubtree(ContentNamespace ns, int publicationId, string taxonomyNodeId,
            int descendantLevels, Ancestor ancestor,
            IContextData contextData)
        {
            try
            {
                var response =
                    _client.Execute<ContentQuery>(GraphQLRequests.SitemapSubtree(ns, publicationId, taxonomyNodeId,
                        descendantLevels, ancestor, contextData, GlobalContextDataInternal));
                return response.TypedResponseData.SitemapSubtree;
            }
            catch (RuntimeBinderException e)
            {
                throw new ApiException(
                    $"Failed to get sitemap subtree (namespaceId:{ns}, publicationId:{publicationId}, taxonomyNodeId{taxonomyNodeId}, descendantLevels:{descendantLevels}",
                    e);
            }
        }

        /// <summary>
        /// Search by raw criteria
        /// </summary>
        /// <param name="rawCriteria">Raw criteria DSL generated by IqQuery API</param>
        /// <param name="resultFilter">Result filter</param>
        /// <param name="pagination">Pagination</param>
        /// <returns>Search results</returns>
        public FacetedSearchResults SearchByRawCriteria(string rawCriteria, InputResultFilter resultFilter, IPagination pagination)
        {
            try
            {
                var response =
                    _client.Execute<ContentQuery>(GraphQLRequests.SearchByRawCriteria(rawCriteria, resultFilter, pagination));
                return response.TypedResponseData.Search;
            }
            catch (RuntimeBinderException e)
            {
                throw new ApiException(
                    $"Failed to get search results (rawCriteria:{rawCriteria})", e);
            }
        }

        /// <summary>
        /// Search by criteria
        /// </summary>
        /// <param name="inputCriteria"></param>
        /// <param name="resultFilter"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        public FacetedSearchResults SearchByCriteria(InputCriteria inputCriteria, InputResultFilter resultFilter, IPagination pagination)
        {
            try
            {
                var response =
                    _client.Execute<ContentQuery>(GraphQLRequests.SearchByCriteria(inputCriteria, resultFilter, pagination));
                return response.TypedResponseData.Search;
            }
            catch (RuntimeBinderException e)
            {
                throw new ApiException(
                    $"Failed to get search results (inputCriteria:{inputCriteria})", e);
            }
        }

        /// <summary>
        /// Faceted Search by criteria
        /// </summary>
        /// <param name="inputCriteria"></param>
        /// <param name="inputFacets"></param>
        /// <param name="resultFilter"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        public FacetedSearchResults FacetedSearch(InputCriteria inputCriteria, InputFacets inputFacets, string language, InputResultFilter resultFilter, IPagination pagination)
        {
            try
            {
                var response =
                    _client.Execute<ContentQuery>(GraphQLRequests.FacetedSearch(inputCriteria, inputFacets, language, resultFilter, pagination));
                return response.TypedResponseData.Search;
            }
            catch (RuntimeBinderException e)
            {
                throw new ApiException(
                    $"Failed to get search results (inputCriteria:{inputCriteria})", e);
            }
        }

        /// <summary>
        /// Suggest - Filter results to match facets used within your content
        /// </summary>
        /// <param name="connectorId"></param>
        /// <param name="label"></param>
        /// <param name="language"></param>
        /// <param name="fuzzy"></param>
        /// <param name="used"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        public ConceptSuggestionConnection Suggest(string label, string language, bool fuzzy, bool used, string connectorId, IPagination pagination)
        {
            try
            {
                var response =
                    _client.Execute<ContentQuery>(GraphQLRequests.Suggest(label, language, fuzzy, used, connectorId, pagination));
                return response.TypedResponseData.Suggest;
            }
            catch (RuntimeBinderException e)
            {
                throw new ApiException(
                    $"Failed to get search results (Suggest:{label})", e);
            }
        }

        #endregion

        #region IPublicContentApiAsync

        public async Task<ComponentPresentation> GetComponentPresentationAsync(ContentNamespace ns, int publicationId,
            int componentId, int templateId,
            string customMetaFilter, ContentIncludeMode contentIncludeMode, IContextData contextData,
            CancellationToken cancellationToken)
            => (await _client.ExecuteAsync<ContentQuery>(
                GraphQLRequests.ComponentPresentation(ns, publicationId, componentId, templateId, customMetaFilter,
                    contentIncludeMode, contextData, GlobalContextDataInternal),
                cancellationToken).ConfigureAwait(false))
                .TypedResponseData.ComponentPresentation;

        public async Task<ComponentPresentationConnection> GetComponentPresentationsAsync(ContentNamespace ns,
            int publicationId, InputComponentPresentationFilter filter,
            InputSortParam sort, IPagination pagination, string customMetaFilter, ContentIncludeMode contentIncludeMode,
            IContextData contextData, CancellationToken cancellationToken)
            => (await _client.ExecuteAsync<ContentQuery>(
                GraphQLRequests.ComponentPresentations(ns, publicationId, filter, sort, pagination, customMetaFilter,
                    contentIncludeMode, contextData, GlobalContextDataInternal),
                cancellationToken).ConfigureAwait(false))
                .TypedResponseData.ComponentPresentations;

        public async Task<Page> GetPageAsync(ContentNamespace ns, int publicationId, int pageId,
            string customMetaFilter, ContentIncludeMode contentIncludeMode, IContextData contextData,
            CancellationToken cancellationToken = default(CancellationToken))
            => (await _client.ExecuteAsync<ContentQuery>(
                GraphQLRequests.Page(ns, publicationId, pageId, customMetaFilter, contentIncludeMode, contextData,
                    GlobalContextDataInternal),
                cancellationToken).ConfigureAwait(false))
                .TypedResponseData.Page;

        public async Task<Page> GetPageAsync(ContentNamespace ns, int publicationId, string url, string customMetaFilter,
            ContentIncludeMode contentIncludeMode,
            IContextData contextData, CancellationToken cancellationToken = default(CancellationToken))
            => (await _client.ExecuteAsync<ContentQuery>(
                GraphQLRequests.Page(ns, publicationId, url, customMetaFilter, contentIncludeMode, contextData,
                    GlobalContextDataInternal),
                cancellationToken).ConfigureAwait(false))
                .TypedResponseData.Page;

        public async Task<Page> GetPageAsync(CmUri cmUri,
            string customMetaFilter, ContentIncludeMode contentIncludeMode, IContextData contextData,
            CancellationToken cancellationToken = default(CancellationToken))
            => (await _client.ExecuteAsync<ContentQuery>(
                GraphQLRequests.Page(cmUri, customMetaFilter, contentIncludeMode, contextData, GlobalContextDataInternal),
                cancellationToken).ConfigureAwait(false))
                .TypedResponseData.Page;

        public async Task<PageConnection> GetPagesAsync(ContentNamespace ns, IPagination pagination, string url,
            string customMetaFilter,
            ContentIncludeMode contentIncludeMode, IContextData contextData,
            CancellationToken cancellationToken = default(CancellationToken))
            => (await _client.ExecuteAsync<ContentQuery>(
                GraphQLRequests.Pages(ns, pagination, url, customMetaFilter, contentIncludeMode, contextData,
                    GlobalContextDataInternal),
                cancellationToken).ConfigureAwait(false))
                .TypedResponseData.Pages;

        public async Task<BinaryComponent> GetBinaryComponentAsync(ContentNamespace ns, int publicationId, int binaryId,
            string customMetaFilter,
            IContextData contextData, CancellationToken cancellationToken = default(CancellationToken)) => (await
                _client.ExecuteAsync<ContentQuery>(
                    GraphQLRequests.BinaryComponent(ns, publicationId, binaryId, customMetaFilter, contextData,
                        GlobalContextDataInternal),
                    cancellationToken).ConfigureAwait(false)).TypedResponseData.BinaryComponent;

        public async Task<BinaryComponent> GetBinaryComponentAsync(ContentNamespace ns, int publicationId, string url,
            string customMetaFilter,
            IContextData contextData, CancellationToken cancellationToken = default(CancellationToken)) => (await
                _client.ExecuteAsync<ContentQuery>(
                    GraphQLRequests.BinaryComponent(ns, publicationId, url, customMetaFilter, contextData,
                        GlobalContextDataInternal),
                    cancellationToken).ConfigureAwait(false)).TypedResponseData.BinaryComponent;

        public async Task<BinaryComponent> GetBinaryComponentAsync(CmUri cmUri, string customMetaFilter,
            IContextData contextData, CancellationToken cancellationToken = default(CancellationToken)) => (await
                _client.ExecuteAsync<ContentQuery>(
                    GraphQLRequests.BinaryComponent(cmUri, customMetaFilter, contextData, GlobalContextDataInternal),
                    cancellationToken).ConfigureAwait(false))
                .TypedResponseData.BinaryComponent;

        public async Task<ItemConnection> ExecuteItemQueryAsync(InputItemFilter filter, InputSortParam sort,
            IPagination pagination, string customMetaFilter, ContentIncludeMode contentIncludeMode,
            bool includeContainerItems,
            IContextData contextData,
            CancellationToken cancellationToken = default(CancellationToken)) => (
                await
                    _client.ExecuteAsync<ContentQuery>(
                        GraphQLRequests.ExecuteItemQuery(filter, sort, pagination, customMetaFilter, contentIncludeMode,
                            includeContainerItems,
                            contextData, GlobalContextDataInternal)
                        , cancellationToken).ConfigureAwait(false)).TypedResponseData.Items;

        public async Task<T> ExecuteExternalItemQueryAsync<T>(string eclUri, string itemType, List<string> itemFields, CancellationToken cancellationToken = default(CancellationToken))
        => (
                await
                    _client.ExecuteAsync<ExternalItemConnection<T>>(
                        GraphQLRequests.BuildExternalItemQuery(eclUri, itemType, itemFields)
                        , cancellationToken).ConfigureAwait(false)).TypedResponseData.ExternalItem;

        public async Task<Publication> GetPublicationAsync(ContentNamespace ns, int publicationId,
            string customMetaFilter,
            IContextData contextData, CancellationToken cancellationToken = default(CancellationToken)) => (
                await
                    _client.ExecuteAsync<ContentQuery>(
                        GraphQLRequests.Publication(ns, publicationId, customMetaFilter, contextData,
                            GlobalContextDataInternal),
                        cancellationToken).ConfigureAwait(false)).TypedResponseData.Publication;

        public async Task<PublicationConnection> GetPublicationsAsync(ContentNamespace ns, IPagination pagination,
            InputPublicationFilter filter, string customMetaFilter, IContextData contextData,
            CancellationToken cancellationToken = default(CancellationToken))
            =>
                (await
                    _client.ExecuteAsync<ContentQuery>(
                        GraphQLRequests.Publications(ns, pagination, filter, customMetaFilter, contextData,
                            GlobalContextDataInternal), cancellationToken).ConfigureAwait(false)).TypedResponseData
                    .Publications;


        public async Task<string> ResolvePageLinkAsync(ContentNamespace ns, int publicationId, int pageId,
            bool renderRelativeLink = true,
            CancellationToken cancellationToken = default(CancellationToken)) => (
                await
                    _client.ExecuteAsync<ContentQuery>(
                        GraphQLRequests.ResolvePageLink(ns, publicationId, pageId, renderRelativeLink),
                        cancellationToken).ConfigureAwait(false)).TypedResponseData.PageLink.Url;

        public async Task<string> ResolveComponentLinkAsync(ContentNamespace ns, int publicationId, int componentId,
            int? sourcePageId,
            int? excludeComponentTemplateId, bool renderRelativeLink = true,
            CancellationToken cancellationToken = default(CancellationToken)) => (
                await
                    _client.ExecuteAsync<ContentQuery>(
                        GraphQLRequests.ResolveComponentLink(ns, publicationId, componentId, sourcePageId,
                            excludeComponentTemplateId, renderRelativeLink), cancellationToken).ConfigureAwait(false))
                .TypedResponseData
                .ComponentLink.Url;

        public async Task<string> ResolveBinaryLinkAsync(ContentNamespace ns, int publicationId, int binaryId,
            string variantId, bool renderRelativeLink = true,
            CancellationToken cancellationToken = default(CancellationToken)) => (
                await
                    _client.ExecuteAsync<ContentQuery>(
                        GraphQLRequests.ResolveBinaryLink(ns, publicationId, binaryId, variantId, renderRelativeLink),
                        cancellationToken).ConfigureAwait(false))
                .TypedResponseData.BinaryLink.Url;

        public async Task<string> ResolveDynamicComponentLinkAsync(ContentNamespace ns, int publicationId, int pageId,
            int componentId,
            int templateId, bool renderRelativeLink = true,
            CancellationToken cancellationToken = default(CancellationToken)) => (
                await
                    _client.ExecuteAsync<ContentQuery>(
                        GraphQLRequests.ResolveDynamicComponentLink(ns, publicationId, pageId, componentId, templateId,
                            renderRelativeLink),
                        cancellationToken).ConfigureAwait(false)).TypedResponseData.DynamicComponentLink.Url;

        public async Task<PublicationMapping> GetPublicationMappingAsync(ContentNamespace ns, string url,
            CancellationToken cancellationToken = default(CancellationToken)) => (await
                _client.ExecuteAsync<ContentQuery>(GraphQLRequests.PublicationMapping(ns, url), cancellationToken)
                    .ConfigureAwait(false))
                .TypedResponseData.PublicationMapping;

        public async Task<dynamic> GetPageModelDataAsync(ContentNamespace ns, int publicationId, int pageId,
            PageInclusion pageInclusion, ContentIncludeMode contentIncludeMode, IContextData contextData,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var response =
                    await
                        _client.ExecuteAsync(
                            GraphQLRequests.PageModelData(ns, publicationId, pageId,
                                pageInclusion, contentIncludeMode, contextData, GlobalContextDataInternal),
                            cancellationToken).ConfigureAwait(false);
                return response.Data.page.rawContent.data;
            }
            catch (RuntimeBinderException e)
            {
                throw new ApiException(
                    $"Failed to get page model data (namespaceId:{ns}, publicationId:{publicationId}, pageId:{pageId}",
                    e);
            }
        }

        public async Task<dynamic> GetPageModelDataAsync(ContentNamespace ns, int publicationId, string url,
            PageInclusion pageInclusion, ContentIncludeMode contentIncludeMode, IContextData contextData,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var response =
                    await
                        _client.ExecuteAsync(
                            GraphQLRequests.PageModelData(ns, publicationId, url, pageInclusion,
                                contentIncludeMode, contextData, GlobalContextDataInternal), cancellationToken)
                            .ConfigureAwait(false);
                return response.Data.page.rawContent.data;
            }
            catch (RuntimeBinderException e)
            {
                throw new ApiException(
                    $"Failed to get page model data (namespaceId:{ns}, publicationId:{publicationId}, url:{url}", e);
            }
        }

        public async Task<dynamic> GetEntityModelDataAsync(ContentNamespace ns, int publicationId, int entityId,
            int templateId, ContentIncludeMode contentIncludeMode, IContextData contextData,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var response =
                    await
                        _client.ExecuteAsync(
                            GraphQLRequests.EntityModelData(ns, publicationId, entityId, templateId,
                                contentIncludeMode, contextData, GlobalContextDataInternal), cancellationToken)
                            .ConfigureAwait(false);
                return response.Data.entity.rawContent.data;
            }
            catch (RuntimeBinderException e)
            {
                throw new ApiException(
                    $"Failed to get enity model data (namespaceId:{ns}, publicationId:{publicationId}, entityId:{entityId}",
                    e);
            }
        }

        public async Task<TaxonomySitemapItem> GetSitemapAsync(ContentNamespace ns, int publicationId,
            int descendantLevels, IContextData contextData,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var response =
                    await
                        _client.ExecuteAsync(
                            GraphQLRequests.Sitemap(ns, publicationId, descendantLevels, contextData,
                                GlobalContextDataInternal),
                            cancellationToken).ConfigureAwait(false);
                return response.Data.sitemap;
            }
            catch (RuntimeBinderException e)
            {
                throw new ApiException(
                    $"Failed to get sitemap (namespaceId:{ns}, publicationId:{publicationId}, descendantLevels:{descendantLevels}",
                    e);
            }
        }

        public async Task<List<TaxonomySitemapItem>> GetSitemapSubtreeAsync(ContentNamespace ns, int publicationId,
            string taxonomyNodeId, int descendantLevels, Ancestor ancestor,
            IContextData contextData, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var response =
                    await
                        _client.ExecuteAsync(
                            GraphQLRequests.SitemapSubtree(ns, publicationId, taxonomyNodeId, descendantLevels,
                                ancestor, contextData, GlobalContextDataInternal), cancellationToken)
                            .ConfigureAwait(false);
                return response.Data.sitemapSubtree;
            }
            catch (RuntimeBinderException e)
            {
                throw new ApiException(
                    $"Failed to get sitemap subtree (namespaceId:{ns}, publicationId:{publicationId}, taxonomyNodeId{taxonomyNodeId}, descendantLevels:{descendantLevels}",
                    e);
            }
        }

        #endregion

        #region Private

        private IContextData GlobalContextDataInternal
        {
            get
            {
                IContextData data = new ContextData(GlobalContextData);
                // Add a default claim here to control model type returned by default
                data.ClaimValues.Add(GraphQLRequests.CreateClaim(DefaultModelType));
                data.ClaimValues.Add(GraphQLRequests.CreateClaim(DefaultContentType));
                // Add claim to control how tcdl links are rendered
                data.ClaimValues.Add(GraphQLRequests.CreateClaim(TcdlLinkRenderingType));
                // Add claim to control how model-service plugin renders links
                data.ClaimValues.Add(GraphQLRequests.CreateClaim(ModelSericeLinkRenderingType));
                // Add claim to control prefix urls
                if (TcdlLinkRenderingType != TcdlLinkRendering.Absolute) return data;
                if (!string.IsNullOrEmpty(TcdlLinkUrlPrefix))
                {
                    data.ClaimValues.Add(GraphQLRequests.CreateClaimTcdlLinkUrlPrefix(TcdlLinkUrlPrefix));
                }
                if (!string.IsNullOrEmpty(TcdlBinaryLinkUrlPrefix))
                {
                    data.ClaimValues.Add(GraphQLRequests.CreateClaimTcdlBinaryLinkUrlPrefix(TcdlBinaryLinkUrlPrefix));
                }
                return data;
            }
        }

        #endregion
    }
}
