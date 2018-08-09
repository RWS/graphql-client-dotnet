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

        Task<BinaryComponent> GetBinaryComponentAsync(ContentNamespace ns, int publicationId, string url, IContextData contextData, CancellationToken cancellationToken);

        Task<BinaryComponent> GetBinaryComponentAsync(CmUri cmUri, IContextData contextData, CancellationToken cancellationToken);

        Task<ItemConnection> ExecuteItemQueryAsync(InputItemFilter filter, InputSortParam sort, IPagination pagination,
            IContextData contextData, string customMetaFilter, bool renderContent, CancellationToken cancellationToken);

        Task<Publication> GetPublicationAsync(ContentNamespace ns, int publicationId, IContextData contextData,
            string customMetaFilter, CancellationToken cancellationToken);

        Task<string> ResolveLinkAsync(CmUri cmUri, bool resolveToBinary, CancellationToken cancellationToken);

        Task<PublicationMapping> GetPublicationMappingAsync(ContentNamespace ns, string url, CancellationToken cancellationToken);
    }
}
