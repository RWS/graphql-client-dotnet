using System.IO;
using System.Reflection;
using System.Resources;

namespace Sdl.Web.GraphQLClient
{
    /// <summary>
    /// GraphQL Queries
    /// </summary>
    public static class QueryResources
    {
        public static string LoadFromResource(string resourceNamespace, string queryName)
        {
            string resourceName = $"{resourceNamespace}.Queries.{queryName}.graphql";
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
}
