using System.Threading;
using System.Threading.Tasks;
using Sdl.Web.PublicContentApi.ContentModel;
using System.Collections.Generic;
using Sdl.Web.PublicContentApi.Utils;

namespace Sdl.Web.PublicContentApi
{
    /// <summary>
    /// Public Content Api (async)
    /// </summary>
    public interface IPublicContentApiAsync
    {
        Task<BinaryComponent> GetBinaryComponentAsync(ContentNamespace ns, int publicationId, int binaryId,
            IContextData contextData, CancellationToken cancellationToken);

        Task<BinaryComponent> GetBinaryComponentAsync(ContentNamespace ns, int publicationId, string url,
            IContextData contextData, CancellationToken cancellationToken);

        Task<BinaryComponent> GetBinaryComponentAsync(CmUri cmUri, IContextData contextData,
            CancellationToken cancellationToken);

        Task<ItemConnection> ExecuteItemQueryAsync(InputItemFilter filter, InputSortParam sort, IPagination pagination,
            IContextData contextData, string customMetaFilter, bool renderContent, CancellationToken cancellationToken);

        Task<Publication> GetPublicationAsync(ContentNamespace ns, int publicationId, IContextData contextData,
            string customMetaFilter, CancellationToken cancellationToken);

        Task<PublicationConnection> GetPublicationsAsync(ContentNamespace ns, IPagination pagination,
            InputPublicationFilter filter,
            IContextData contextData, string customMetaFilter, CancellationToken cancellationToken);

        Task<string> ResolvePageLinkAsync(ContentNamespace ns, int publicationId, int pageId,
            CancellationToken cancellationToken);

        Task<string> ResolveComponentLinkAsync(ContentNamespace ns, int publicationId, int componentId,
            int? sourcePageId, int? excludeComponentTemplateId, CancellationToken cancellationToken);

        Task<string> ResolveBinaryLinkAsync(ContentNamespace ns, int publicationId, int binaryId, string variantId,
            CancellationToken cancellationToken);

        Task<string> ResolveDynamicComponentLinkAsync(ContentNamespace ns, int publicationId, int pageId,
            int componentId, int templateId, CancellationToken cancellationToken);

        Task<PublicationMapping> GetPublicationMappingAsync(ContentNamespace ns, string url,
            CancellationToken cancellationToken);

        Task<dynamic> GetPageModelDataAsync(ContentNamespace ns, int publicationId, string url, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, bool renderContent, IContextData contextData,
            CancellationToken cancellationToken);

        Task<dynamic> GetPageModelDataAsync(ContentNamespace ns, int publicationId, int pageId, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, bool renderContent, IContextData contextData,
            CancellationToken cancellationToken);

        Task<dynamic> GetEntityModelDataAsync(ContentNamespace ns, int publicationId, int entityId, int templateId,
            ContentType contentType,
            DataModelType modelType, DcpType dcpType, bool renderContent, IContextData contextData,
            CancellationToken cancellationToken);

        Task<TaxonomySitemapItem> GetSitemapAsync(ContentNamespace ns, int publicationId, int descendantLevels,
            IContextData contextData, CancellationToken cancellationToken);

        Task<TaxonomySitemapItem> GetSitemapSubtreeAsync(ContentNamespace ns, int publicationId, string taxonomyNodeId,
            int descendantLevels, bool includeAncestors,
            IContextData contextData, CancellationToken cancellationToken);
    }
}
