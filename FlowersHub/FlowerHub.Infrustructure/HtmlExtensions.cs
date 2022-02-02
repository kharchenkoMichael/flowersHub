using System.Collections.Generic;
using HtmlAgilityPack;

namespace FlowersHub.Infrastructure
{
    public static class HtmlExtensions
    {
        public static string[] SplitAndReplace(this string str)
        {
            return str.ToLower()
                .Replace(".", "")
                .Replace(",", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace(":", "")
                .Replace("!", "")
                .Replace("- ", "")
                .Split(' ');
        }
        public static List<HtmlNode> GetByClass(this HtmlNode item, string c)
        {
            var result = new List<HtmlNode>();

            foreach (var itemClass in item.GetClasses())
            {
                if (itemClass == c)
                {
                    result.Add(item);
                    break;
                }
            }

            foreach (var child in item.ChildNodes)
            {
                result.AddRange(GetByClass(child, c));
            }

            return result;
        }

        public static List<string> GetAllClasses(this HtmlNode item)
        {
            var result = new List<string>();

            foreach (var itemClass in item.GetClasses())
                result.Add(itemClass);

            foreach (var child in item.ChildNodes)
                result.AddRange(GetAllClasses(child));

            return result;
        }
    }
}
