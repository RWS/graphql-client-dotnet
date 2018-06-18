using System.Collections.Generic;

namespace Sdl.Web.PublicContentApi.ContentModel
{
    public class ContextData : IContextData
    {
        public List<ClaimValue> ClaimValues { get; set; }
    }
}
