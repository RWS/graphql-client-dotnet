using System.Collections.Generic;

namespace Sdl.Tridion.Api.Client.ContentModel.Patches
{
    public class ContentComponent : IContentComponent
    {
        public string CreationDate { get; set; }
        public CustomMetaConnection CustomMetas { get; set; }
        public string Id { get; set; }
        public string InitialPublishDate { get; set; }
        public int ItemId { get; set; }
        public ItemType ItemType { get; set; }
        public string LastPublishDate { get; set; }
        public ContentNamespace NamespaceId { get; set; }
        public int? OwningPublicationId { get; set; }
        public int PublicationId { get; set; }
        public int? SchemaId { get; set; }
        public List<ITaxonomyItem> Taxonomies { get; set; }
        public string Title { get; set; }
        public string UpdatedDate { get; set; }
        public bool? MultiMedia { get; set; }
    }
}
