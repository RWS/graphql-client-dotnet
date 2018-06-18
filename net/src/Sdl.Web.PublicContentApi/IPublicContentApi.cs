using System.Collections.Generic;
using Sdl.Web.PublicContentApi.ContentModel;

namespace Sdl.Web.PublicContentApi
{
    public interface IPublicContentApi
    {
        // Binary Components
        BinaryComponent GetBinaryComponent(ContentNamespace ns, int publicationId, int binaryId, IContextData contextData);
        BinaryComponent GetBinaryComponent(ContentNamespace ns, int publicationId, string url, IContextData contextData);

        // Categories/Keywords
        List<Keyword> GetKeywords(ContentNamespace ns, int publicationId, IPagination pagination, IContextData contextData);
        Keyword GetKeyword(ContentNamespace ns, int publicationId, int categoryId, int keywordId, IContextData contextData);

        // Component Presentations
        ComponentPresentation GetComponentPresentation(ContentNamespace ns, int publicationId, int componentId,
            int templateId, IContextData contextData);

        // Items
        List<IItem> GetItems(IItemFilter itemFiter, IPagination pagination, IContextData contextData);

        // Publications
        Publication GetPublication(ContentNamespace ns, int publicationId, IContextData contextData);
        List<Publication> GetPublications(ContentNamespace ns, IPagination pagination, IPublicationFilter publicationFilter, IContextData contextData);
        object GetPublicationMapping(ContentNamespace ns, string uri, IContextData contextData);

        // Pages
        List<Page> GetPages(ContentNamespace ns, string url, IPagination pagination, IContextData contextData);

        // Structure Groups
        List<StructureGroup> GetStructureGroups(ContentNamespace ns, int publicationId, int structureGroupId,
           IPagination pagination, IContextData contextData);
        StructureGroup GetStructureGroup(ContentNamespace ns, int publicationId, int structureGroupId,
            IContextData contextData);
    }
}
