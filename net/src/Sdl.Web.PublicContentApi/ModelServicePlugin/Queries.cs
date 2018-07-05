namespace Sdl.Web.PublicContentApi.ModelServicePlugin
{
    /// <summary>
    /// Model Service GraphQL Queries
    /// </summary>
    public static class Queries
    {
        public static string Load(string queryName) 
            => Web.PublicContentApi.Queries.LoadFromResource("Sdl.Web.PublicContentApi.ModelServicePlugin", queryName);
    }
}
