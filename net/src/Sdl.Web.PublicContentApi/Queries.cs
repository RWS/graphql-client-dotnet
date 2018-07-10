using System.Linq;

namespace Sdl.Web.PublicContentApi
{
    /// <summary>
    /// GraphQL Queries
    /// </summary>
    public static class Queries
    {      
        public static string Load(params string[] queryNames) 
            => queryNames.Aggregate(string.Empty, (current, q) => current + GraphQLClient.QueryResources.LoadFromResource("Sdl.Web.PublicContentApi", q));
    }
}
