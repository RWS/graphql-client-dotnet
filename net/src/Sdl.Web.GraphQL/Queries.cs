namespace Sdl.Web.GraphQLClient
{
    public static class Queries
    {
        public static string Load(string queryName, bool loadFragments)
            => QueryResources.LoadQueryFromResource("Sdl.Web.GraphQLClient", queryName, loadFragments);
    }
}
