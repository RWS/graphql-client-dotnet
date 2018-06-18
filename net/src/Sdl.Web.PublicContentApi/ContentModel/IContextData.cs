using System.Collections.Generic;

namespace Sdl.Web.PublicContentApi.ContentModel
{
    public interface IContextData
    {
        List<ClaimValue> ClaimValues { get; set; }
    }
}
