using System.Threading;
using System.Threading.Tasks;
using Sdl.Tridion.Api.Client.ContentModel;
using System.Collections.Generic;
using Sdl.Tridion.Api.Client.Utils;

namespace Sdl.Tridion.Api.Client
{
    /// <summary>
    /// Content Api (async)
    /// </summary>
    public interface IApiClientAsync
    {
        Task<ComponentPresentation> GetComponentPresentationAsync(ContentNamespace ns, int publicationId,
            int componentId,
            int templateId, string customMetaFilter, ContentIncludeMode contentIncludeMode, IContextData contextData,
            CancellationToken cancellationToken);

        Task<ComponentPresentationConnection> GetComponentPresentationsAsync(ContentNamespace ns, int publicationId,
            InputComponentPresentationFilter filter,
            InputSortParam sort,
            IPagination pagination,
            string customMetaFilter, ContentIncludeMode contentIncludeMode, IContextData contextData,
            CancellationToken cancellationToken);

        Task<Page> GetPageAsync(ContentNamespace ns, int publicationId, int pageId, string customMetaFilter,
            ContentIncludeMode contentIncludeMode,
            IContextData contextData, CancellationToken cancellationToken);

        Task<Page> GetPageAsync(ContentNamespace ns, int publicationId, string url, string customMetaFilter,
            ContentIncludeMode contentIncludeMode,
            IContextData contextData, CancellationToken cancellationToken);

        Task<Page> GetPageAsync(CmUri cmUri, string customMetaFilter,
            ContentIncludeMode contentIncludeMode,
            IContextData contextData, CancellationToken cancellationToken);

        Task<PageConnection> GetPagesAsync(ContentNamespace ns, IPagination pagination, string url,
            string customMetaFilter, ContentIncludeMode contentIncludeMode, IContextData contextData,
            CancellationToken cancellationToken);

        Task<BinaryComponent> GetBinaryComponentAsync(ContentNamespace ns, int publicationId, int binaryId,
            string customMetaFilter,
            IContextData contextData, CancellationToken cancellationToken);

        Task<BinaryComponent> GetBinaryComponentAsync(ContentNamespace ns, int publicationId, string url,
            string customMetaFilter,
            IContextData contextData, CancellationToken cancellationToken);

        Task<BinaryComponent> GetBinaryComponentAsync(CmUri cmUri, string customMetaFilter, IContextData contextData,
            CancellationToken cancellationToken);

        Task<ItemConnection> ExecuteItemQueryAsync(InputItemFilter filter, InputSortParam sort, IPagination pagination,
            string customMetaFilter, ContentIncludeMode contentIncludeMode, bool includeContainerItems,
            IContextData contextData,
            CancellationToken cancellationToken);

        Task<T> ExecuteExternalItemQueryAsync<T>(string eclUri, string itemType, List<string> itemFields, CancellationToken cancellationToken);

        Task<Publication> GetPublicationAsync(ContentNamespace ns, int publicationId, string customMetaFilter,
            IContextData contextData, CancellationToken cancellationToken);

        Task<PublicationConnection> GetPublicationsAsync(ContentNamespace ns, IPagination pagination,
            InputPublicationFilter filter, string customMetaFilter, IContextData contextData,
            CancellationToken cancellationToken);

        Task<string> ResolvePageLinkAsync(ContentNamespace ns, int publicationId, int pageId, bool renderRelativeLink,
            CancellationToken cancellationToken);

        Task<string> ResolveComponentLinkAsync(ContentNamespace ns, int publicationId, int componentId,
            int? sourcePageId, int? excludeComponentTemplateId, bool renderRelativeLink,
            CancellationToken cancellationToken);

        Task<string> ResolveBinaryLinkAsync(ContentNamespace ns, int publicationId, int binaryId, string variantId,
            bool renderRelativeLink,
            CancellationToken cancellationToken);

        Task<string> ResolveDynamicComponentLinkAsync(ContentNamespace ns, int publicationId, int pageId,
            int componentId, int templateId, bool renderRelativeLink, CancellationToken cancellationToken);

        Task<PublicationMapping> GetPublicationMappingAsync(ContentNamespace ns, string url,
            CancellationToken cancellationToken);

        Task<dynamic> GetPageModelDataAsync(ContentNamespace ns, int publicationId, string url,
            PageInclusion pageInclusion, ContentIncludeMode contentIncludeMode,
            IContextData contextData,
            CancellationToken cancellationToken);

        Task<dynamic> GetPageModelDataAsync(ContentNamespace ns, int publicationId, int pageId,
            PageInclusion pageInclusion, ContentIncludeMode contentIncludeMode, IContextData contextData,
            CancellationToken cancellationToken);

        Task<dynamic> GetEntityModelDataAsync(ContentNamespace ns, int publicationId, int entityId, int templateId,
            ContentIncludeMode contentIncludeMode, IContextData contextData,
            CancellationToken cancellationToken);

        Task<TaxonomySitemapItem> GetSitemapAsync(ContentNamespace ns, int publicationId, int descendantLevels,
            IContextData contextData, CancellationToken cancellationToken);

        Task<List<TaxonomySitemapItem>> GetSitemapSubtreeAsync(ContentNamespace ns, int publicationId,
            string taxonomyNodeId,
            int descendantLevels, Ancestor ancestor,
            IContextData contextData, CancellationToken cancellationToken);
    }
}
