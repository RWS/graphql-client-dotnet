using System.Collections.Generic;
using Sdl.Tridion.Api.Client.ContentModel;
using Sdl.Tridion.Api.Client.Utils;
using Sdl.Tridion.Api.GraphQL.Client.Response;

namespace Sdl.Tridion.Api.Client
{
    /// <summary>
    /// Content Api
    /// </summary>
    public interface IApiClient
    {
        /// <summary>
        /// Holds global context data passed on to PCA service. Note that context data passed
        /// directly to API methods overwrites these values.
        /// </summary>
        IContextData GlobalContextData { get; set; }

        /// <summary>
        /// Specify type of content to return from API. When set to RAW no conversion will take
        /// place otherwise its treated as model data and will go through conversion to type specified
        /// by DefaultModelType (default: Model)
        /// </summary>
        ContentType DefaultContentType { get; set; }

        /// <summary>
        /// Specify model type to return (default: R2)
        /// </summary>
        DataModelType DefaultModelType { get; set; }

        /// <summary>
        /// Specify how tcdl links get rendered (default: relative)
        /// </summary>
        TcdlLinkRendering TcdlLinkRenderingType { get; set; }

        /// <summary>
        /// Specify how the model-service plugin renders links (default: relative)
        /// </summary>
        ModelServiceLinkRendering ModelSericeLinkRenderingType { get; set; }

        /// <summary>
        /// Specify Url prefix for tcdl links for Absolute rendering type (default: none)
        /// </summary>
        string TcdlLinkUrlPrefix { get; set; }

        /// <summary>
        /// Specify Url prefix for tcdl binary links for Absolute rendering type (default: none)
        /// </summary>
        string TcdlBinaryLinkUrlPrefix { get; set; }

        /// <summary>
        /// Specify if the GraphQL client should throw on any error or try to continue deserialization.
        /// </summary>
        bool ThrowOnAnyError { get; set; }

        /// <summary>
        /// Returns true if any errors were found during last GraphQL request.
        /// </summary>
        bool HasErrors { get; }

        /// <summary>
        /// Return list of GraphQL errors.
        /// </summary>
        List<GraphQLError> LastErrors { get; }

        /// <summary>
        /// Returns the component presentation given the namespace, publication, component and template IDs.
        /// </summary>
        /// <param name="ns">Namespace</param>
        /// <param name="publicationId">Publication Id</param>
        /// <param name="componentId">Component Id</param>
        /// <param name="templateId">Template Id</param>
        /// <param name="customMetaFilter">Custom Meta Filter</param>
        /// <param name="contentIncludeMode">Specify if content should be included</param>
        /// <param name="contextData">Context data</param>
        /// <returns>Component Presentation</returns>
        ComponentPresentation GetComponentPresentation(ContentNamespace ns, int publicationId, int componentId,
            int templateId, string customMetaFilter, ContentIncludeMode contentIncludeMode, IContextData contextData);

        /// <summary>
        /// Get a list of component presentations filtered by set of criteria.
        /// </summary>
        /// <param name="ns">Namespace</param>
        /// <param name="publicationId">Publication Id</param>
        /// <param name="filter">Filter</param>
        /// <param name="sort">Sort Order</param>
        /// <param name="pagination">Pagination</param>
        /// <param name="customMetaFilter">Custom Meta Filter</param>
        /// <param name="contentIncludeMode">Specify if content should be included</param>
        /// <param name="contextData">Context data</param>
        /// <returns></returns>
        ComponentPresentationConnection GetComponentPresentations(ContentNamespace ns, int publicationId,
            InputComponentPresentationFilter filter,
            InputSortParam sort,
            IPagination pagination,
            string customMetaFilter, ContentIncludeMode contentIncludeMode, IContextData contextData);

        /// <summary>
        /// Returns the page given its page ID
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="publicationId"></param>
        /// <param name="pageId"></param>
        /// <param name="customMetaFilter"></param>
        /// <param name="contentIncludeMode"></param>
        /// <param name="contextData"></param>
        /// <returns></returns>
        Page GetPage(ContentNamespace ns, int publicationId, int pageId, string customMetaFilter,
            ContentIncludeMode contentIncludeMode,
            IContextData contextData);

        /// <summary>
        /// Return page by Url.
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="publicationId"></param>
        /// <param name="url"></param>
        /// <param name="customMetaFilter"></param>
        /// <param name="contentIncludeMode"></param>
        /// <param name="contextData"></param>
        /// <returns></returns>
        Page GetPage(ContentNamespace ns, int publicationId, string url, string customMetaFilter,
            ContentIncludeMode contentIncludeMode,
            IContextData contextData);

        /// <summary>
        /// Return page by CM Uri.
        /// </summary>
        /// <param name="cmUri"></param>
        /// <param name="customMetaFilter"></param>
        /// <param name="contentIncludeMode"></param>
        /// <param name="contextData"></param>
        /// <returns></returns>
        Page GetPage(CmUri cmUri, string customMetaFilter,
            ContentIncludeMode contentIncludeMode,
            IContextData contextData);

        /// <summary>
        /// Returns the list of pages matching the provided namespace ID and page URL.
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="pagination"></param>
        /// <param name="url"></param>
        /// <param name="customMetaFilter"></param>
        /// <param name="contentIncludeMode"></param>
        /// <param name="contextData"></param>
        /// <returns></returns>
        PageConnection GetPages(ContentNamespace ns, IPagination pagination, string url,
            string customMetaFilter, ContentIncludeMode contentIncludeMode, IContextData contextData);

        /// <summary>
        /// Returns the binary component given a binary ID.
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="publicationId"></param>
        /// <param name="binaryId"></param>
        /// <param name="customMetaFilter"></param>
        /// <param name="contextData"></param>
        /// <returns></returns>
        BinaryComponent GetBinaryComponent(ContentNamespace ns, int publicationId, int binaryId, string customMetaFilter,
            IContextData contextData);

        /// <summary>
        /// Returns the binary component given a Url.
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="publicationId"></param>
        /// <param name="url"></param>
        /// <param name="customMetaFilter"></param>
        /// <param name="contextData"></param>
        /// <returns></returns>
        BinaryComponent GetBinaryComponent(ContentNamespace ns, int publicationId, string url, string customMetaFilter,
            IContextData contextData);

        /// <summary>
        /// Returns the binary component given a CM Uri.
        /// </summary>
        /// <param name="cmUri"></param>
        /// <param name="customMetaFilter"></param>
        /// <param name="contextData"></param>
        /// <returns></returns>
        BinaryComponent GetBinaryComponent(CmUri cmUri, string customMetaFilter, IContextData contextData);

        /// <summary>
        /// Execute item query given a filter.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pagination"></param>
        /// <param name="customMetaFilter"></param>
        /// <param name="contentIncludeMode"></param>
        /// <param name="includeContainerItems"></param>
        /// <param name="contextData"></param>
        /// <returns></returns>
        ItemConnection ExecuteItemQuery(InputItemFilter filter, InputSortParam sort, IPagination pagination,
            string customMetaFilter, ContentIncludeMode contentIncludeMode, bool includeContainerItems,
            IContextData contextData);

        /// <summary>
        /// Execute external item query given a filter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eclUri"></param>
        /// <param name="itemType"></param>
        /// <param name="itemFields"></param>
        /// <returns></returns>
        T ExecuteExternalItemQuery<T>(string eclUri, string itemType, List<string> itemFields);

        /// <summary>
        /// Get publication given a publication ID.
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="publicationId"></param>
        /// <param name="customMetaFilter"></param>
        /// <param name="contextData"></param>
        /// <returns></returns>
        Publication GetPublication(ContentNamespace ns, int publicationId, string customMetaFilter,
            IContextData contextData);

        /// <summary>
        /// Get a list of publications given a filter.
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="pagination"></param>
        /// <param name="filter"></param>
        /// <param name="customMetaFilter"></param>
        /// <param name="contextData"></param>
        /// <returns></returns>
        PublicationConnection GetPublications(ContentNamespace ns, IPagination pagination, InputPublicationFilter filter,
            string customMetaFilter,
            IContextData contextData);

        /// <summary>
        /// Resolve page link.
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="publicationId"></param>
        /// <param name="pageId"></param>
        /// <param name="renderRelativeLink"></param>
        /// <returns></returns>
        string ResolvePageLink(ContentNamespace ns, int publicationId, int pageId, bool renderRelativeLink);

        /// <summary>
        /// Resolve component link.
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="publicationId"></param>
        /// <param name="componentId"></param>
        /// <param name="sourcePageId"></param>
        /// <param name="excludeComponentTemplateId"></param>
        /// <param name="renderRelativeLink"></param>
        /// <returns></returns>
        string ResolveComponentLink(ContentNamespace ns, int publicationId, int componentId, int? sourcePageId,
            int? excludeComponentTemplateId, bool renderRelativeLink);

        /// <summary>
        /// Resolve binary link.
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="publicationId"></param>
        /// <param name="binaryId"></param>
        /// <param name="variantId"></param>
        /// <param name="renderRelativeLink"></param>
        /// <returns></returns>
        string ResolveBinaryLink(ContentNamespace ns, int publicationId, int binaryId, string variantId,
            bool renderRelativeLink);

        /// <summary>
        /// Resolve dynamic component link.
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="publicationId"></param>
        /// <param name="pageId"></param>
        /// <param name="componentId"></param>
        /// <param name="templateId"></param>
        /// <param name="renderRelativeLink"></param>
        /// <returns></returns>
        string ResolveDynamicComponentLink(ContentNamespace ns, int publicationId, int pageId, int componentId,
            int templateId, bool renderRelativeLink);

        /// <summary>
        /// Get publication mapping for url.
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        PublicationMapping GetPublicationMapping(ContentNamespace ns, string url);

        /// <summary>
        /// Get page model data given a Url.
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="publicationId"></param>
        /// <param name="url"></param>
        /// <param name="pageInclusion"></param>
        /// <param name="contentIncludeMode"></param>
        /// <param name="contextData"></param>
        /// <returns></returns>
        dynamic GetPageModelData(ContentNamespace ns, int publicationId, string url,
            PageInclusion pageInclusion, ContentIncludeMode contentIncludeMode,
            IContextData contextData);

        /// <summary>
        /// Get page model data given a page ID
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="publicationId"></param>
        /// <param name="pageId"></param>
        /// <param name="pageInclusion"></param>
        /// <param name="contentIncludeMode"></param>
        /// <param name="contextData"></param>
        /// <returns></returns>
        dynamic GetPageModelData(ContentNamespace ns, int publicationId, int pageId,
            PageInclusion pageInclusion, ContentIncludeMode contentIncludeMode, IContextData contextData);

        /// <summary>
        /// Get entity model data given an entity and template ID.
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="publicationId"></param>
        /// <param name="entityId"></param>
        /// <param name="templateId"></param>
        /// <param name="contentIncludeMode"></param>
        /// <param name="contextData"></param>
        /// <returns></returns>
        dynamic GetEntityModelData(ContentNamespace ns, int publicationId, int entityId, int templateId,
            ContentIncludeMode contentIncludeMode, IContextData contextData);

        /// <summary>
        /// Get sitemap.
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="publicationId"></param>
        /// <param name="descendantLevels"></param>
        /// <param name="contextData"></param>
        /// <returns></returns>
        TaxonomySitemapItem GetSitemap(ContentNamespace ns, int publicationId, int descendantLevels,
            IContextData contextData);

        /// <summary>
        /// Get sitemap subtree for a specific taxonomy node.
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="publicationId"></param>
        /// <param name="taxonomyNodeId"></param>
        /// <param name="descendantLevels"></param>
        /// <param name="ancestor"></param>
        /// <param name="contextData"></param>
        /// <returns></returns>
        List<TaxonomySitemapItem> GetSitemapSubtree(ContentNamespace ns, int publicationId, string taxonomyNodeId,
            int descendantLevels, Ancestor ancestor,
            IContextData contextData);


        /// <summary>
        /// Search by raw criteria
        /// </summary>
        /// <param name="rawCriteria">Raw criteria DSL generated by IqQuery API</param>
        /// <param name="resultFilter">Result filter</param>
        /// <param name="pagination">Pagination</param>
        /// <returns>Search results</returns>
        FacetedSearchResults SearchByRawCriteria(string rawCriteria, InputResultFilter resultFilter, IPagination pagination);

        /// <summary>
        /// Search by criteria
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="resultFilter"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        FacetedSearchResults SearchByCriteria(InputCriteria criteria, InputResultFilter resultFilter, IPagination pagination);

        /// <summary>
        /// Faceted Search by criteria
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="inputFacets"></param>
        /// <param name="resultFilter"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        FacetedSearchResults FacetedSearch(InputCriteria criteria, InputFacets inputFacets, string language, InputResultFilter resultFilter, IPagination pagination);

        /// <summary>
        /// Suggest - Filter results to match facets used within your content
        /// </summary>
        /// <param name="label"></param>
        /// <param name="langauage"></param>
        /// <param name="fuzzy"></param>
        /// <param name="used"></param>
        /// <param name="connectorId"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        ConceptSuggestionConnection Suggest(string label, string langauage, bool fuzzy, bool used, string connectorId, IPagination pagination);
    }
}