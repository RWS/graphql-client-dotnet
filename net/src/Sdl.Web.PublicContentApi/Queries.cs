namespace Sdl.Web.PublicContentApi
{
    /// <summary>
    /// GraphQL Queries
    /// </summary>
    public static class Queries
    {
        public static readonly string GetBinaryComponentByUrl = @"
            TODO
        ";

        public static readonly string GetBinaryComponentById = @"
            TODO
        ";

        public static readonly string GetKeywords = @"
            query categories($namespaceId: Int! $publicationId: Int! $first: Int $after: String) {
                categories(namespaceId: $namespaceId, publicationId: $publicationId, first: $first, after: $after) {
                    edges {
                        cursor
                        node {
                            key
                            description
                            name
                            lastPublishDate
                        }
                    }
                }
            }";

        public static readonly string GetKeyword = @"
            TODO
        ";

        public static readonly string GetComponentPresentation = @"
            TODO
        ";

        public static readonly string GetPublication = @"
            TODO
        ";

        public static readonly string GetPublications = @"
            query publications($namespaceId: Int! $first: Int $after: String $filterCustomMeta: String) {
                publications(namespaceId: $namespaceId, first: $first, after: $after) {
                    edges {
                        cursor
                        node {
                            id
                            creationDate
                            initialPublishDate
                            itemId
                            customMetas(filter: $filterCustomMeta) {
                                edges {
                                    node {
                                        id
                                        itemId
                                        key
                                        namespaceId
                                        publicationId
                                        value
                                        valueType
                                     }
                                }
                            }
                            itemId
                            itemType
                            lastPublishDate
                            namespaceId
                            owningPublicationId
                            publicationId
                            title
                            updatedDate
                         }
                    }
                }
            }
        ";

        public static readonly string GetPublicationMapping = @"
            TODO
        ";

        public static readonly string GetPages = @"
            TODO
        ";

        public static readonly string GetStructureGroups = @"
            TODO
        ";

        public static readonly string GetStructureGroup = @"
            TODO
        ";
    }
}
