using System.Collections.Generic;
using Sdl.Web.PublicContentApi.ContentModel;
using Sdl.Web.PublicContentApi.Utils;

namespace Sdl.Web.PublicContentApi
{
    /// <summary>
    /// Public Content Api
    /// </summary>
    public interface IPublicContentApi
    {
        /// <summary>
        /// Holds global context data passed on to PCA service. Note that context data passed
        /// directly to API methods overwrites these values.
        /// </summary>
        IContextData GlobalContextData { get; set; }

        /// <summary>
        /// Specify type of content to return from API. When set to RAW no conversion will take
        /// place otherwise its treated as model data and will go through conversion to type specified
        /// by DefaultModelType
        /// </summary>
        ContentType DefaultContentType { get; set; }

        /// <summary>
        /// Specify model type to return
        /// </summary>
        DataModelType DefaultModelType { get; set; }

        /// <summary>
        /// Specify how tcdl links get rendered
        /// </summary>
        TcdlLinkRendering TcdlLinkRenderingType { get; set; }

        /// <summary>
        /// Specify how the model-service plugin renders links
        /// </summary>
        ModelServiceLinkRendering ModelSericeLinkRenderingType { get; set; }

        ComponentPresentation GetComponentPresentation(ContentNamespace ns, int publicationId, int componentId,
            int templateId, string customMetaFilter, ContentIncludeMode contentIncludeMode, IContextData contextData);

        ComponentPresentationConnection GetComponentPresentations(ContentNamespace ns, int publicationId,
            InputComponentPresentationFilter filter,
            InputSortParam sort,
            IPagination pagination,
            string customMetaFilter, ContentIncludeMode contentIncludeMode, IContextData contextData);

        Page GetPage(ContentNamespace ns, int publicationId, int pageId, string customMetaFilter,
            ContentIncludeMode contentIncludeMode,
            IContextData contextData);

        Page GetPage(ContentNamespace ns, int publicationId, string url, string customMetaFilter,
            ContentIncludeMode contentIncludeMode,
            IContextData contextData);

        Page GetPage(ContentNamespace ns, int publicationId, CmUri cmUri, string customMetaFilter,
            ContentIncludeMode contentIncludeMode,
            IContextData contextData);

        PageConnection GetPages(ContentNamespace ns, IPagination pagination, string url,
            string customMetaFilter, ContentIncludeMode contentIncludeMode, IContextData contextData);

        BinaryComponent GetBinaryComponent(ContentNamespace ns, int publicationId, int binaryId, string customMetaFilter,
            IContextData contextData);

        BinaryComponent GetBinaryComponent(ContentNamespace ns, int publicationId, string url, string customMetaFilter,
            IContextData contextData);

        BinaryComponent GetBinaryComponent(CmUri cmUri, string customMetaFilter, IContextData contextData);

        ItemConnection ExecuteItemQuery(InputItemFilter filter, InputSortParam sort, IPagination pagination,
            string customMetaFilter, ContentIncludeMode contentIncludeMode, bool includeContainerItems,
            IContextData contextData);

        Publication GetPublication(ContentNamespace ns, int publicationId, string customMetaFilter,
            IContextData contextData);

        PublicationConnection GetPublications(ContentNamespace ns, IPagination pagination, InputPublicationFilter filter,
            string customMetaFilter,
            IContextData contextData);

        string ResolvePageLink(ContentNamespace ns, int publicationId, int pageId, bool renderRelativeLink);

        string ResolveComponentLink(ContentNamespace ns, int publicationId, int componentId, int? sourcePageId,
            int? excludeComponentTemplateId, bool renderRelativeLink);

        string ResolveBinaryLink(ContentNamespace ns, int publicationId, int binaryId, string variantId,
            bool renderRelativeLink);

        string ResolveDynamicComponentLink(ContentNamespace ns, int publicationId, int pageId, int componentId,
            int templateId, bool renderRelativeLink);

        PublicationMapping GetPublicationMapping(ContentNamespace ns, string url);

        dynamic GetPageModelData(ContentNamespace ns, int publicationId, string url,
            PageInclusion pageInclusion, ContentIncludeMode contentIncludeMode,
            IContextData contextData);

        dynamic GetPageModelData(ContentNamespace ns, int publicationId, int pageId,
            PageInclusion pageInclusion, ContentIncludeMode contentIncludeMode, IContextData contextData);

        dynamic GetEntityModelData(ContentNamespace ns, int publicationId, int entityId, int templateId,
            ContentIncludeMode contentIncludeMode, IContextData contextData);

        TaxonomySitemapItem GetSitemap(ContentNamespace ns, int publicationId, int descendantLevels,
            IContextData contextData);

        List<TaxonomySitemapItem> GetSitemapSubtree(ContentNamespace ns, int publicationId, string taxonomyNodeId,
            int descendantLevels, Ancestor ancestor,
            IContextData contextData);
    }
}