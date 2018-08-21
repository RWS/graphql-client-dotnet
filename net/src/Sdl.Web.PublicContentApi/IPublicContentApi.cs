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

        BinaryComponent GetBinaryComponent(ContentNamespace ns, int publicationId, int binaryId,
            IContextData contextData);

        BinaryComponent GetBinaryComponent(ContentNamespace ns, int publicationId, string url, IContextData contextData);

        BinaryComponent GetBinaryComponent(CmUri cmUri, IContextData contextData);

        ItemConnection ExecuteItemQuery(InputItemFilter filter, InputSortParam sort, IPagination pagination,
            IContextData contextData, string customMetaFilter, bool renderContent);

        Publication GetPublication(ContentNamespace ns, int publicationId, IContextData contextData,
            string customMetaFilter);

        string ResolvePageLink(ContentNamespace ns, int publicationId, int pageId);

        string ResolveComponentLink(ContentNamespace ns, int publicationId, int componentId, int? sourcePageId, int? excludeComponentTemplateId);

        string ResolveBinaryLink(ContentNamespace ns, int publicationId, int binaryId, string variantId);

        string ResolveDynamicComponentLink(ContentNamespace ns, int publicationId, int pageId, int componentId, int templateId);

        PublicationMapping GetPublicationMapping(ContentNamespace ns, string url);

        dynamic GetPageModelData(ContentNamespace ns, int publicationId, string url, ContentType contentType,
           DataModelType modelType, PageInclusion pageInclusion, bool renderContent, IContextData contextData);

        dynamic GetPageModelData(ContentNamespace ns, int publicationId, int pageId, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, bool renderContent, IContextData contextData);

        dynamic GetEntityModelData(ContentNamespace ns, int publicationId, int entityId, int templateId, ContentType contentType,
            DataModelType modelType, DcpType dcpType, bool renderContent, IContextData contextData);

        TaxonomySitemapItem GetSitemap(ContentNamespace ns, int publicationId, int descendantLevels, IContextData contextData);

        TaxonomySitemapItem GetSitemapSubtree(ContentNamespace ns, int publicationId, string taxonomyNodeId, int descendantLevels, bool includeAncestors,
            IContextData contextData);
    }
}
