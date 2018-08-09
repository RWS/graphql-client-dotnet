using Sdl.Web.PublicContentApi.ContentModel;

namespace Sdl.Web.PublicContentApi.Utils
{
    public static class ContentModelHelpers
    {
        public static CmUri CmUri(this IItem item)
            => new CmUri(item.NamespaceId, item.PublicationId, item.ItemId, item.ItemType);
    }
}
