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
        public static string LoadFromResource(string resourceNamespace, string queryName)
        {
            string resourceName = $"{resourceNamespace}.Queries.{queryName}.graphql";
            using (Stream stm = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stm != null)
                {
                    return new StreamReader(stm).ReadToEnd();
                }
            }
            throw new MissingManifestResourceException($"Resource {resourceName} not found");
        }

        public static string Load(params string[] queryNames) 
            => queryNames.Aggregate(string.Empty, (current, q) => current + Queries.LoadFromResource("Sdl.Web.PublicContentApi", q));
    }
}
