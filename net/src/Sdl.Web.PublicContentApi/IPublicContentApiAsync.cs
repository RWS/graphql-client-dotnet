using System.Threading;
using System.Threading.Tasks;
using Sdl.Web.PublicContentApi.ContentModel;

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

        Task<KeywordConnection> GetKeywordsAsync(ContentNamespace ns, int publicationId, IPagination pagination,
            IContextData contextData, CancellationToken cancellationToken);

        Task<Keyword> GetKeywordAsync(ContentNamespace ns, int publicationId, int categoryId, int keywordId,
            IContextData contextData, CancellationToken cancellationToken);

        Task<ComponentPresentation> GetComponentPresentationAsync(ContentNamespace ns, int publicationId,
            int componentId,
            int templateId, IContextData contextData, CancellationToken cancellationToken);

        Task<ItemConnection> GetItemsAsync(InputItemFilter filter, IPagination pagination, IContextData contextData,
            CancellationToken cancellationToken);

        Task<Publication> GetPublicationAsync(ContentNamespace ns, int publicationId, IContextData contextData,
            CancellationToken cancellationToken);

        Task<PublicationConnection> GetPublicationsAsync(ContentNamespace ns, IPagination pagination,
            InputPublicationFilter publicationFilter, IContextData contextData, CancellationToken cancellationToken);

        Task<object> GetPublicationMappingAsync(ContentNamespace ns, string uri, IContextData contextData,
            CancellationToken cancellationToken);

        Task<PageConnection> GetPagesAsync(ContentNamespace ns, string url, IPagination pagination,
            IContextData contextData, CancellationToken cancellationToken);

        Task<StructureGroupConnection> GetStructureGroupsAsync(ContentNamespace ns, int publicationId,
            IPagination pagination, IContextData contextData, CancellationToken cancellationToken);

        Task<StructureGroup> GetStructureGroupAsync(ContentNamespace ns, int publicationId, int structureGroupId,
            IContextData contextData, CancellationToken cancellationToken);
    }
}
