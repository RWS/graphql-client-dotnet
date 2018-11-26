using Sdl.Tridion.Api.GraphQL.Client;

namespace Sdl.Tridion.Api.Client
{
    /// <summary>
    /// GraphQL Queries
    /// </summary>
    public static class Queries
    {
        public static string Load(string queryName, bool loadFragments) 
            => QueryResources.LoadQueryFromResource("Sdl.Tridion.Api.Client", queryName, loadFragments);

        public static string LoadFragments(string query) 
            => QueryResources.LoadFragments("Sdl.Tridion.Api.Client", query);
    }
}
