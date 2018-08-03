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

        ItemConnection ExecuteItemQuery(InputItemFilter filter, IPagination pagination,
            IContextData contextData, string customMetaFilter, bool renderContent);

        Publication GetPublication(ContentNamespace ns, int publicationId, IContextData contextData,
            string customMetaFilter);

        string ResolveLink(CmUri cmUri, bool resolveToBinary);

        PublicationMapping GetPublicationMapping(ContentNamespace ns, string url);
    }
}
