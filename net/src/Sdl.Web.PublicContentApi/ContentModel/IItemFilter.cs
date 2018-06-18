using System.Collections.Generic;

namespace Sdl.Web.PublicContentApi.ContentModel
{
    public interface IItemFilter
    {
        List<int> NamespaceIds { get; set; }
        List<int> PublicationIds { get; set; }

        List<IItemFilter> And { get; set; }
        List<IItemFilter> Or { get; set; }
    }
}
