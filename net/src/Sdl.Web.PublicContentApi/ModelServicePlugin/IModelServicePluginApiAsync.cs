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
        Task<dynamic> GetPageModelDataAsync(ContentNamespace ns, int publicationId, string url, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, bool renderContent, IContextData contextData, CancellationToken cancellationToken);
     
        Task<dynamic> GetPageModelDataAsync(ContentNamespace ns, int publicationId, int pageId, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, bool renderContent, IContextData contextData, CancellationToken cancellationToken);

        Task<dynamic> GetEntityModelDataAsync(ContentNamespace ns, int publicationId, int entityId, ContentType contentType,
            DataModelType modelType, DcpType dcpType, bool renderContent, IContextData contextData, CancellationToken cancellationToken);

        Task<TaxonomySitemapItem> GetSitemapAsync(ContentNamespace ns, int publicationId, int descendantLevels, IContextData contextData, CancellationToken cancellationToken);

        Task<TaxonomySitemapItem> GetSitemapSubtreeAsync(ContentNamespace ns, int publicationId, string taxonomyNodeId, int descendantLevels, bool includeAncestors,
            IContextData contextData, CancellationToken cancellationToken);
    }
}
