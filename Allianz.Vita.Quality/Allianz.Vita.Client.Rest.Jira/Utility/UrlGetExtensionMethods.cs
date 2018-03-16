using Allianz.Vita.Client.Rest.Jira.DataModel.Intefaces;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allianz.Vita.Client.Rest.Jira.Utility
{
    static class UrlGetExtensionMethods
    {
        static int UrlLengthLimit = 2000;

        public static string UrlGetCombine(this string baseUrl, RequestItem item)
        {
            List<string> output = item.ToString().Split(',').ToList().ConvertAll<string>(HtmlEncoder);
            return string.Concat(baseUrl, "?", string.Join("&", output));
        }

        private static string HtmlEncoder(string input)
        {
            return HttpUtility.HtmlEncode(input);
        }

        public static bool IsComplexQuery(this string baseUrl)
        {
            return baseUrl.Contains("&") || baseUrl.Contains("\"");
        }

        public static bool ExcedUrlLimit(this string baseUrl)
        {
            return UrlLengthLimit > 0 && baseUrl.Length > UrlLengthLimit;
        }

    }
}
