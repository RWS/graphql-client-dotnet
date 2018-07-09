using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace Sdl.Web.PublicContentApi
{
    /// <summary>
    /// GraphQL Queries
    /// </summary>
    public static class Queries
    {      
        public static string Load(params string[] queryNames) 
            => queryNames.Aggregate(string.Empty, (current, q) => current + GraphQLClient.Queries.Queries.LoadFromResource("Sdl.Web.PublicContentApi", q));
    }
}
