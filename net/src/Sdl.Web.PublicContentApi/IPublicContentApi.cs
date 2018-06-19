using Sdl.Web.PublicContentApi.ContentModel;

namespace Sdl.Web.PublicContentApi
{
    /// <summary>
    /// Public Content Api
    /// </summary>
    public interface IPublicContentApi
    {
        // Binary Components
        BinaryComponent GetBinaryComponent(ContentNamespace ns, int publicationId, int binaryId,
            IContextData contextData);

        BinaryComponent GetBinaryComponent(ContentNamespace ns, int publicationId, string url, IContextData contextData);

        // Categories/Keywords
        KeywordConnection GetKeywords(ContentNamespace ns, int publicationId, IPagination pagination,
            IContextData contextData);

        Keyword GetKeyword(ContentNamespace ns, int publicationId, int categoryId, int keywordId,
            IContextData contextData);

        // Component Presentations
        ComponentPresentation GetComponentPresentation(ContentNamespace ns, int publicationId, int componentId,
            int templateId, IContextData contextData);

        // Items
        ItemConnection GetItems(InputItemFilter filter, IPagination pagination, IContextData contextData);

        // Publications
        Publication GetPublication(ContentNamespace ns, int publicationId, IContextData contextData);

        PublicationConnection GetPublications(ContentNamespace ns, IPagination pagination,
            InputPublicationFilter publicationFilter, IContextData contextData);

        object GetPublicationMapping(ContentNamespace ns, string uri, IContextData contextData);

        // Pages
        PageConnection GetPages(ContentNamespace ns, string url, IPagination pagination, IContextData contextData);

        // Structure Groups
        StructureGroupConnection GetStructureGroups(ContentNamespace ns, int publicationId,
            IPagination pagination, IContextData contextData);

        StructureGroup GetStructureGroup(ContentNamespace ns, int publicationId, int structureGroupId,
            IContextData contextData);
    }
}
