using Sdl.Tridion.Api.Client.ContentModel;

namespace Sdl.Tridion.Api.Client.Utils
{
    public static class ContentModelHelpers
    {
        public static CmUri CmUri(this IItem item)
            => new CmUri(item.NamespaceId, item.PublicationId, item.ItemId, item.ItemType);
    }
}
