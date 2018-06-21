using System.Collections.Generic;
using Sdl.Web.PublicContentApi.ContentModel;

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
      
        ItemConnection ExecuteItemQuery(InputItemFilter filter, IPagination pagination,
            List<InputClaimValue> contextData, string customMetaFilter);
       
        Publication GetPublication(ContentNamespace ns, int publicationId, List<InputClaimValue> contextData, string customMetaFilter);       
    }
}
