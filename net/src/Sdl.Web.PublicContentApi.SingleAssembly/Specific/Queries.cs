using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using Sdl.Web.GraphQLClient;

namespace Sdl.Web.PublicContentApi
{
    public static class Queries
    {
        public static string Load(string queryName)
            => QueryResources.LoadFromResource("Sdl.Web.PublicContentApi", queryName);

        public static string Load(params string[] queryNames)
            =>
                queryNames.Aggregate(string.Empty,
                    (current, q) =>
                        current + QueryResources.LoadFromResource("Sdl.Web.PublicContentApi", q));
    }
}

namespace Sdl.Web.GraphQLClient
{
    public static class QueryResources
    {
        public static string LoadFromResource(string resourceNamespace, string queryName)
        {
            string resourceName = $"{resourceNamespace}.{queryName}.graphql";
            using (Stream stm = Assembly.GetCallingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stm != null)
                {
                    return new StreamReader(stm).ReadToEnd();
                }
            }
            throw new MissingManifestResourceException($"Resource {resourceName} not found");
        }
    }

    public static class Queries
    {
        public static string Load(string queryName)
            => QueryResources.LoadFromResource("Sdl.Web.PublicContentApi", queryName);

        public static string Load(params string[] queryNames)
            =>
                queryNames.Aggregate(string.Empty,
                    (current, q) =>
                        current + QueryResources.LoadFromResource("Sdl.Web.PublicContentApi", q));
    }
}
