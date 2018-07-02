namespace Sdl.Web.PublicContentApi.ModelServicePlugin
{
    /// <summary>
    /// Model Service GraphQL Queries
    /// </summary>
    public static class Queries
    {
        public static readonly string GetPageModelDataByUrl = @"
            query page($namespaceId: Int! $publicationId: Int! $url: String) {
                page(namespaceId: $namespaceId, publicationId: $publicationId, url: $url) {
                    itemId                
                    rawContent(renderContent: false) {
                      data 
                    }
                }
            }
        ";

        public static readonly string GetPageModelDataByPageId = @"
            query page($pageId: Int! $namespaceId: Int! $publicationId: Int!) {
                page(pageId: $pageId, namespaceId: $namespaceId, publicationId: $publicationId) {
                    itemId                
                    rawContent(renderContent: false) {
                      data 
                    }
                }
            }
        ";

        public static readonly string GetEntityModelData = @"
            query componentPresentation($namespaceId: Int!, $publicationId: Int!, $componentId: Int!) {
                componentPresentation(namespaceId: $namespaceId, publicationId: $publicationId, componentId: $componentId) {
                    itemId            
                    rawContent(renderContent: false) {
                        data
                    }
                }
            }
        ";

        public static readonly string GetSitemap = @"
            query sitemap($namespaceId: Int! $publicationId: Int!) {          
              sitemap(namespaceId: $namespaceId, publicationId: $publicationId) {
                ...taxonomyItemFields               
                ...recurseItems
              }
            }
        ";

        public static readonly string GetSitemapSubtree = @"
            query sitemapSubtree($namespaceId: Int! $publicationId: Int! $taxonomyNodeId: String!) {          
              sitemapSubtree(namespaceId: $namespaceId, publicationId: $publicationId, taxonomyNodeId: $taxonomyNodeId) {
                ...taxonomyItemFields               
                ...recurseItems
              }
            }
        ";

        public static readonly string GetSitemapFragments = @"            
            fragment recurseItems on TaxonomySitemapItem {  
              items {
                ...taxonomyItemFields  
                ...on TaxonomySitemapItem {
                  items {
                    ...taxonomyItemFields
                    ...taxonomyPageFields
                    ...on TaxonomySitemapItem {
                      items {
                        ...taxonomyItemFields
                        ...taxonomyPageFields
                      }      
                    }
                  }
                }
              }
            }

            fragment taxonomyItemFields on TaxonomySitemapItem {
                id
                title
                originalTitle
                url
                type
                publishedDate
                visible
                description
                key
                abstract
                hasChildNodes
            }
            
            fragment taxonomyPageFields on PageSitemapItem {
                id
                title
                originalTitle
                url
                type
                publishedDate
                visible  
            }
        ";       
    }
}
