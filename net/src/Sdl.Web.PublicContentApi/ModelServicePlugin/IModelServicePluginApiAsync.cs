using System.Threading;
using System.Threading.Tasks;
using Sdl.Web.PublicContentApi.ContentModel;

namespace Sdl.Web.PublicContentApi.ModelServicePlugin
{
    /// <summary>
    /// Model Service Plugin Api (async)
    /// </summary>
    public interface IModelServicePluginApiAsync
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
        Task<object> GetPageModelDataAsync(ContentNamespace ns, int publicationId, string url, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, IContextData contextData, CancellationToken cancellationToken);

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
        Task<object> GetPageModelDataAsync(ContentNamespace ns, int publicationId, int pageId, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, IContextData contextData, CancellationToken cancellationToken);

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
        Task<object> GetEntityModelDataAsync(ContentNamespace ns, int publicationId, int entityId, ContentType contentType,
            DataModelType modelType, DcpType dcpType, IContextData contextData, CancellationToken cancellationToken);

        /// <summary>
        /// Get Sitemap
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="publicationId"></param>
        /// <param name="contextData"></param>
        /// <returns></returns>
        Task<object> GetSitemap(ContentNamespace ns, int publicationId, IContextData contextData, CancellationToken cancellationToken);

        /// <summary>
        /// Get Sitemap
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="publicationId"></param>
        /// <param name="taxonomyNodeId"></param>
        /// <param name="includeAncestors"></param>
        /// <param name="contextData"></param>
        /// <returns></returns>
        //Taxonomy Node ID in format: t<taxonomy ID>-k<keyword ID>, e.g. ‘t222-k1038'
        Task<object> GetSitemapAsync(ContentNamespace ns, int publicationId, string taxonomyNodeId, bool includeAncestors,
            IContextData contextData, CancellationToken cancellationToken);
    }
}
