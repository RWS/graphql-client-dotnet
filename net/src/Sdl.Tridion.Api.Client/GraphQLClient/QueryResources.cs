using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Sdl.Tridion.Api.GraphQL.Client
{
    /// <summary>
    /// GraphQL Query Resource Handling
    /// </summary>
    public static class QueryResources
    {            
        public static string LoadQueryFromResource(string resourceNamespace, string queryName, bool loadFragments)
        {
            string resourceName = $"{resourceNamespace}.Queries.{queryName}.graphql";
            Assembly callingAssembly = Assembly.GetCallingAssembly();
            using (Stream stm = callingAssembly.GetManifestResourceStream(resourceName))
            {
                if (stm == null) return string.Empty;
                string q = new StreamReader(stm).ReadToEnd();
                return loadFragments ? LoadFragments(callingAssembly, resourceNamespace, q) : q;
            }
        }

        private static string LoadFragmentFromResource(Assembly callingAssembly, string resourceNamespace, string fragmentName)
        {
            string resourceName = $"{resourceNamespace}.Queries.Fragments.{fragmentName}.graphql";
            using (Stream stm = callingAssembly.GetManifestResourceStream(resourceName))
            {
                if (stm != null) return new StreamReader(stm).ReadToEnd();
            }
            return string.Empty;
        }

        public static string LoadFragments(string resourceNamespace, string query)
        {
            return LoadFragments(Assembly.GetCallingAssembly(), resourceNamespace, query);
        }

        private static string LoadFragments(Assembly callingAssembly, string resourceNamespace, string query)
        {
            string newQuery = query;
            HashSet<string> loaded = new HashSet<string>();
            int s = 0;
            while (s >= 0)
            {
                s = newQuery.IndexOf("...", s);

                for (int e = s + 3; s >= 0 && e < newQuery.Length; e++)
                {
                    if (char.IsLetter(newQuery[e])) continue;
                    string fragmentName = newQuery.Substring(s + 3, e - s - 3);
                    if (!loaded.Contains(fragmentName))
                    {
                        loaded.Add(fragmentName);
                        newQuery += LoadFragmentFromResource(callingAssembly, resourceNamespace, fragmentName);
                    }
                    s = e;
                    break;
                }
            }
            return newQuery;
        }    
    }
}
