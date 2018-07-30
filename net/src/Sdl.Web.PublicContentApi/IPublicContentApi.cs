using System.Collections.Generic;
using Sdl.Web.PublicContentApi.ContentModel;
using Sdl.Web.PublicContentApi.Utils;

namespace Sdl.Web.PublicContentApi
{
    /// <summary>
    /// Public Content Api
    /// </summary>
    public interface IPublicContentApi
    {
        BinaryComponent GetBinaryComponent(ContentNamespace ns, int publicationId, int binaryId,
            IContextData contextData);

        BinaryComponent GetBinaryComponent(ContentNamespace ns, int publicationId, string url, IContextData contextData);

        BinaryComponent GetBinaryComponent(CmUri cmUri, IContextData contextData);

        ItemConnection ExecuteItemQuery(InputItemFilter filter, IPagination pagination,
            List<InputClaimValue> contextData, string customMetaFilter);

        Publication GetPublication(ContentNamespace ns, int publicationId, List<InputClaimValue> contextData,
            string customMetaFilter);

        string ResolveLink(CmUri cmUri, bool resolveToBinary);

        PublicationMapping GetPublicationMapping(ContentNamespace ns, string url);
    }
}
