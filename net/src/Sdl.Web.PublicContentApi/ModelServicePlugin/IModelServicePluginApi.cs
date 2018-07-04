using Sdl.Web.PublicContentApi.ContentModel;

namespace Sdl.Web.PublicContentApi.ModelServicePlugin
{
    /// <summary>
    /// Model Service Plugin Api
    /// </summary>
    public interface IModelServicePluginApi
    {
        /// <summary>
        /// Get Page Model Data
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="publicationId"></param>
        /// <param name="url"></param>
        /// <param name="contentType"></param>
        /// <param name="modelType"></param>
        /// <param name="pageInclusion"></param>
        /// <param name="contextData"></param>
        /// <returns></returns>
        dynamic GetPageModelData(ContentNamespace ns, int publicationId, string url, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, IContextData contextData);

        /// <summary>
        /// Get Page Model Data
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="publicationId"></param>
        /// <param name="pageId"></param>
        /// <param name="contentType"></param>
        /// <param name="modelType"></param>
        /// <param name="pageInclusion"></param>
        /// <param name="contextData"></param>
        /// <returns></returns>
        dynamic GetPageModelData(ContentNamespace ns, int publicationId, int pageId, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, IContextData contextData);

        /// <summary>
        /// Get Entity Model Data
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="publicationId"></param>
        /// <param name="entityId"></param>
        /// <param name="contentType"></param>
        /// <param name="modelType"></param>
        /// <param name="dcpType"></param>
        /// <param name="contextData"></param>
        /// <returns></returns>
        dynamic GetEntityModelData(ContentNamespace ns, int publicationId, int entityId, ContentType contentType,
            DataModelType modelType, DcpType dcpType, IContextData contextData);

        /// <summary>
        /// Get Sitemap
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="publicationId"></param>
        /// <param name="descendantLevels"></param>
        /// <param name="contextData"></param>
        /// <returns></returns>
        TaxonomySitemapItem GetSitemap(ContentNamespace ns, int publicationId, int descendantLevels, IContextData contextData);

        /// <summary>
        /// Get Sitemap Subtree
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="publicationId"></param>
        /// <param name="taxonomyNodeId">Taxonomy Node ID in format: t[taxonomyID]-k[keywordID], e.g. ‘t222-k1038'</param>
        /// <param name="descendantLevels"></param>
        /// <param name="contextData"></param>
        /// <returns></returns>
        TaxonomySitemapItem GetSitemapSubtree(ContentNamespace ns, int publicationId, string taxonomyNodeId, int descendantLevels,
            IContextData contextData);
    }
}
