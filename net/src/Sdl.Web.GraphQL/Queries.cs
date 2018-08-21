namespace Sdl.Web.GraphQLClient
{
    /// <summary>
    /// Load queries by name from inbuilt resources
    /// </summary>
    public static class Queries
    {
        /// <summary>
        /// Load query from embedded resources.
        /// </summary>
        /// <param name="queryName">Name fo query</param>
        /// <param name="loadFragments">True to also auto-load referenced fragements in query</param>
        /// <returns></returns>
        public static string Load(string queryName, bool loadFragments)
            => QueryResources.LoadQueryFromResource("Sdl.Web.GraphQLClient", queryName, loadFragments);
    }
}
