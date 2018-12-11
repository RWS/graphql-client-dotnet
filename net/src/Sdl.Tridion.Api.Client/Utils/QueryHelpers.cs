using System;
using System.Text;

namespace Sdl.Tridion.Api.Client.Utils
{
    /// <summary>
    /// This class provides simple helper functions to clean up and handle some special custom graphQL script
    /// features.
    /// TODO: Should clean this up and replace with regexps at some point.
    /// </summary>
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
            int index = query.IndexOf($"{regionName}?", StringComparison.Ordinal);
            if (index == -1) return query;
            StringBuilder sb = new StringBuilder(query.Length);
            int lastIndex = 0;
            while (index >= 0)
            {
                sb.Append(query.Substring(lastIndex, index - lastIndex));
                int start = query.IndexOf("{", index + regionName.Length, StringComparison.Ordinal) + 1;
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
                index = query.IndexOf($"{regionName}?", lastIndex, StringComparison.Ordinal);
                if (index < 0)
                {
                    sb.Append(query.Substring(lastIndex));
                }
            }
            return sb.ToString();
        }

        public static void ExpandRecursiveFragment(ref string query, string fragmentToExpand, int decendentLevel)
        {
            int rFragIndex = query.IndexOf("rfragment", StringComparison.Ordinal);
            if (rFragIndex == -1) return;
            do
            {
                int s = query.IndexOf(" ", rFragIndex, StringComparison.Ordinal);
                int e = query.IndexOf(" on", s, StringComparison.Ordinal);
                string fragmentName = query.Substring(s, e - s).Trim();
                if (fragmentToExpand != null && !fragmentName.Equals(fragmentToExpand)) continue;
                int n = 1;
                string fragmentBody = "";
                int rFragBodyStartIndex = query.IndexOf("{", e, StringComparison.Ordinal) + 1;
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
                rFragIndex = query.IndexOf("rfragment", rFragBodyStartIndex + 1, StringComparison.Ordinal);
            } while (rFragIndex >= 0);
        }

        public static void RemoveUnusedFragments(ref string query)
        {
            if (string.IsNullOrEmpty(query)) return;
            int index = query.IndexOf("fragment ", StringComparison.Ordinal);
            if (index == -1) return;
            while (index >= 0)
            {
                int s = query.IndexOf(" ", index, StringComparison.Ordinal);
                int e = query.IndexOf(" on", s, StringComparison.Ordinal);
                var fragmentName = query.Substring(s, e - s).Trim();
                if (query.IndexOf($"...{fragmentName}", StringComparison.Ordinal) == -1)
                {
                    Tuple<int, int> indices = query.FindOpenCloseBrace(e, '{', '}');
                    if(indices != null)
                        query = query.Substring(0, index) + query.Substring(indices.Item2 + 1);
                }
                index = query.IndexOf("fragment ", index + 1, StringComparison.Ordinal);
            }
        }
    }
}
