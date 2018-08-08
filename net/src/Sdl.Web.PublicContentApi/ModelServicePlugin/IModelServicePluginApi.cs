using Sdl.Web.PublicContentApi.ContentModel;

namespace Sdl.Web.PublicContentApi.ModelServicePlugin
{
    /// <summary>
    /// Model Service Plugin Api
    /// </summary>
    public interface IModelServicePluginApi
    {        
        dynamic GetPageModelData(ContentNamespace ns, int publicationId, string url, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, bool renderContent, IContextData contextData);

        dynamic GetPageModelData(ContentNamespace ns, int publicationId, int pageId, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, bool renderContent, IContextData contextData);

        dynamic GetEntityModelData(ContentNamespace ns, int publicationId, int entityId, ContentType contentType,
            DataModelType modelType, DcpType dcpType, bool renderContent, IContextData contextData);

        TaxonomySitemapItem GetSitemap(ContentNamespace ns, int publicationId, int descendantLevels, IContextData contextData);

        TaxonomySitemapItem GetSitemapSubtree(ContentNamespace ns, int publicationId, string taxonomyNodeId, int descendantLevels, bool includeAncestors,
            IContextData contextData);
    }
}
