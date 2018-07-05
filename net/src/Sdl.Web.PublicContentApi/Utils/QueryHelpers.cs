using System.Text;

namespace Sdl.Web.PublicContentApi.Utils
{
    public static class QueryHelpers
    {
        public static void ExpandRecursiveFragment(ref string query, string fragmentToExpand, int decendentLevel)
        {
            int rFragIndex = query.IndexOf("rfragment");
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
