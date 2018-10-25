using System.Text;

namespace Sdl.Tridion.Api.Client.Utils
{
    public static class QueryHelpers
    {
        /// <summary>
        /// Remove regions from a query based on include parameter given the following query syntax:
        ///  {regionName}? {
        ///     query region to exclude/include
        ///  }
        /// </summary>
        /// <param name="query">Query</param>
        /// <param name="regionName">Region name</param>
        /// <param name="include">Determine if we should include/exclude</param>
        /// <returns>Rebuilt query</returns>
        public static string ParseIncludeRegions(string query, string regionName, bool include)
        {
            if (string.IsNullOrEmpty(query)) return query;
            int index = query.IndexOf($"{regionName}?");
            if (index == -1) return query;
            StringBuilder sb = new StringBuilder();
            int lastIndex = 0;
            while (index >= 0)
            {
                sb.Append(query.Substring(lastIndex, index - lastIndex));
                int start = query.IndexOf("{", index + regionName.Length) + 1;
                int n = 1;
                int end;
                for (end = start; n > 0; end++)
                {
                    switch (query[end])
                    {
                        case '{':
                            n++;
                            break;
                        case '}':
                            n--;
                            break;
                    }
                }

                if (include)
                {
                    sb.Append(query.Substring(start, end - start - 1));
                }
                lastIndex = end;
                index = query.IndexOf($"{regionName}?", lastIndex);
                if (index < 0)
                {
                    sb.Append(query.Substring(lastIndex));
                }
            }
            return sb.ToString();
        }

        public static void ExpandRecursiveFragment(ref string query, string fragmentToExpand, int decendentLevel)
        {
            int rFragIndex = query.IndexOf("rfragment");
            if (rFragIndex == -1) return;
            do
            {
                int s = query.IndexOf(" ", rFragIndex);
                int e = query.IndexOf("on", s);
                string fragmentName = query.Substring(s, e - s).Trim();
                if (fragmentToExpand != null && !fragmentName.Equals(fragmentToExpand)) continue;
                int n = 1;
                string fragmentBody = "";
                int rFragBodyStartIndex = query.IndexOf("{", e) + 1;
                int rFragBodyLength = 0;
                for (int i = rFragBodyStartIndex; i < query.Length; i++)
                {
                    if (query[i] == '{')
                    {
                        n++;
                    }
                    else if (query[i] == '}')
                    {
                        n--;
                    }
                    if (n != 0) continue;
                    rFragBodyLength = i - rFragBodyStartIndex;
                    fragmentBody = query.Substring(rFragBodyStartIndex, rFragBodyLength);
                    break;
                }
                string expanded = string.Empty;
                query = new StringBuilder(query) {[rFragIndex] = ' '}.ToString();
                if (decendentLevel > 0)
                {
                    for (int i = 0; i < decendentLevel; i++)
                    {
                        expanded = string.IsNullOrEmpty(expanded)
                            ? fragmentBody
                            : expanded.Replace($"...{fragmentName}", fragmentBody);
                    }
                }
                else
                {
                    expanded = fragmentBody;
                }
                expanded = expanded.Replace($"...{fragmentName}", "");
                string lhs = query.Substring(0, rFragBodyStartIndex);
                string rhs = query.Substring(rFragBodyStartIndex + rFragBodyLength);
                query = lhs + expanded + rhs;
                rFragIndex = query.IndexOf("rfragment", rFragBodyStartIndex + 1);
            } while (rFragIndex >= 0);
        }
    }
}
