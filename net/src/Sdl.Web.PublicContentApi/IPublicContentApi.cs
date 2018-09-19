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
        IContextData GlobalContextData { get; set; }

        Page GetPage(ContentNamespace ns, int publicationId, int pageId, string customMetaFilter,
            IContextData contextData);

        Page GetPage(ContentNamespace ns, int publicationId, string url, string customMetaFilter,
            IContextData contextData);

        Page GetPage(ContentNamespace ns, int publicationId, CmUri cmUri, string customMetaFilter,
            IContextData contextData);

        BinaryComponent GetBinaryComponent(ContentNamespace ns, int publicationId, int binaryId,
            IContextData contextData);

        BinaryComponent GetBinaryComponent(ContentNamespace ns, int publicationId, string url, IContextData contextData);

        BinaryComponent GetBinaryComponent(CmUri cmUri, IContextData contextData);

        ItemConnection ExecuteItemQuery(InputItemFilter filter, InputSortParam sort, IPagination pagination,
            string customMetaFilter, bool renderContent, bool includeContainerItems, IContextData contextData);

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

        dynamic GetPageModelData(ContentNamespace ns, int publicationId, string url, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, bool renderContent, IContextData contextData);

        dynamic GetPageModelData(ContentNamespace ns, int publicationId, int pageId, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, bool renderContent, IContextData contextData);

        dynamic GetEntityModelData(ContentNamespace ns, int publicationId, int entityId, int templateId,
            ContentType contentType,
            DataModelType modelType, DcpType dcpType, bool renderContent, IContextData contextData);

        TaxonomySitemapItem GetSitemap(ContentNamespace ns, int publicationId, int descendantLevels,
            IContextData contextData);

        List<TaxonomySitemapItem> GetSitemapSubtree(ContentNamespace ns, int publicationId, string taxonomyNodeId,
            int descendantLevels, bool includeAncestors,
            IContextData contextData);
    }
}