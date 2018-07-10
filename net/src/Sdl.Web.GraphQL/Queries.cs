namespace Sdl.Web.GraphQLClient
{
    public static class Queries
    {
        public static string Load(string queryName)
            => QueryResources.LoadFromResource("Sdl.Web.GraphQLClient", queryName);
    }
}
