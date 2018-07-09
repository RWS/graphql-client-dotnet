namespace Sdl.Web.PublicContentApi.ModelServicePlugin
{
    /// <summary>
    /// Model Service GraphQL Queries
    /// </summary>
    public static class Queries
    {
        public static string Load(string queryName) 
            => GraphQLClient.Queries.Queries.LoadFromResource("Sdl.Web.PublicContentApi.ModelServicePlugin", queryName);
    }
}
