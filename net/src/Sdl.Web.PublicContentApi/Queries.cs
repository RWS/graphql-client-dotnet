namespace Sdl.Web.PublicContentApi
{
    /// <summary>
    /// GraphQL Queries
    /// </summary>
    public static class Queries
    {
        public static string Load(string queryName, bool loadFragments) 
            => GraphQLClient.QueryResources.LoadQueryFromResource("Sdl.Web.PublicContentApi", queryName, loadFragments);

        public static string LoadFragments(string query) 
            => GraphQLClient.QueryResources.LoadFragments("Sdl.Web.PublicContentApi", query);
    }
}
