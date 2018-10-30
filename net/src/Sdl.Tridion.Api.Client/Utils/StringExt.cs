using System;
using System.Linq;

namespace Sdl.Tridion.Api.Client.Utils
{
    public static class StringExt
    {
        public static string PascalCase(this string word)
        {
            return string.Join(" ", word.Split(' ')
                         .Select(w => w.Trim())
                         .Where(w => w.Length > 0)
                         .Select(w => w.Substring(0, 1).ToUpper() + w.Substring(1)));
        }

        public static string Capitialize(this string word)
        {
            return string.Join("", word.Split(' ', '_')
                         .Select(w => w.Trim())
                         .Where(w => w.Length > 0)
                         .Select(w => w.Substring(0, 1).ToUpper() + w.Substring(1).ToLower()));
        }

        public static bool IsCmUri(this string str) => CmUri.IsCmUri(str);

        public static Tuple<int,int> FindOpenCloseBrace(this string str, int startIndex, char openBrace, char closingBrace)
        {
            var a = str.IndexOf(openBrace.ToString(), startIndex, StringComparison.Ordinal) + 1;
            var n = 1;
            for (var i = a; i < str.Length; i++)
            {
                if (str[i] == openBrace)
                    n++;
                else if (str[i] == closingBrace)
                    n--;
                if (n != 0) continue;
                return new Tuple<int, int>(a, i);
            }
            return null;
        }
    }
}
