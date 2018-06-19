namespace Sdl.Web.PublicContentApi.ModelServicePlugin
{
    /// <summary>
    /// Model Service GraphQL Queries
    /// </summary>
    public static class Queries
    {
        public static readonly string GetPageModelDataByUrl = @"
            TODO
        ";

        public static readonly string GetPageModelDataByPageId = @"
            query page($pageId: Int! $namespaceId: Int! $publicationId: Int!) {
                page(pageId: 6437, namespaceId: 1, publicationId: 178) {
                    itemId                
                    rawContent(renderContent: false) {
                      content
                      charSet
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
                        content
                        charSet
                        data
                    }
                }
            }
        ";

        public static readonly string GetSitemap = @"
            query sitemap($namespaceId: Int! $publicationId: Int) {          
              sitemap(namespaceId: $namespaceId, publicationId: $publicationId) {
                id
                title
                originalTitle
                url
                type
                visible
                publishedDate
                hasChildNodes
                items {
                  id
                  title
                  originalTitle
                    url
                  visible
                  type
                  ... on TaxonomySitemapItem {
                    hasChildNodes
                    items {
                      id
                      title
                      originalTitle
                      url
                      visible
                      type
                      ... on TaxonomySitemapItem {
                        hasChildNodes
                        items {
                          id
                          title
                          url
                          visible
                          type
                          publishedDate
                        }
                      }
                    }
                  }
                }
              }
        ";

        public static readonly string GetSitemapSubtree = @"
            TODO
        ";
    }
}
