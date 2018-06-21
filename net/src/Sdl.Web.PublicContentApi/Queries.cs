namespace Sdl.Web.PublicContentApi
{
    /// <summary>
    /// GraphQL Queries
    /// </summary>
    public static class Queries
    {
        public static readonly string CustomMetaField = @"
            customMetas(filter: {0}) {{
                edges {{
                    node {{
                        key
                        value
                        valueType
                    }}
                }}     
            }}
        ";

        public static readonly string GetBinaryComponentByUrl = @"
            TODO
        ";

        public static readonly string GetBinaryComponentById = @"
            TODO
        ";

        public static readonly string ItemsQuery = @"
            query items($first: Int $after: String $filter: InputItemFilter! $contextData: [InputClaimValue!]) {{
                items(first: $first, after: $after, filter: $filter, contextData: $contextData) {{
                    edges {{
                        cursor
                        node {{
                            id
                            itemId
                            itemType
                            creationDate
                            initialPublishDate
                            lastPublishDate
                            namespaceId
                            owningPublicationId
                            publicationId
                            title
                            updatedDate                                 
                            {0}
                            ... on Publication {{
                                publicationUrl
                                publicationPath
                                multimediaPath
                                multimediaUrl          
                            }}
                            ... on Component {{
                                schemaId
                                multiMedia                            
                            }}
                        }}
                    }}
                }}
            }}
        ";     

        public static readonly string GetPublication = @"
            query publication($namespaceId: Int! $publicationId: Int! $contextData: [InputClaimValue!]) {{
                publication(namespaceId: $namespaceId, publicationId: $publicationId, contextData: $contextData) {{
                    id
                    itemId
                    itemType
                    creationDate
                    initialPublishDate
                    lastPublishDate
                    namespaceId
                    owningPublicationId
                    publicationId
                    title
                    updatedDate     
                    publicationUrl
                    publicationPath
                    multimediaPath
                    multimediaUrl
                    {0}             
                }}
            }}
        ";    
    }
}
