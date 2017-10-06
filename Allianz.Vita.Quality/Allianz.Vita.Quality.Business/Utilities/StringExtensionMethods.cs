using System.Linq;

namespace System
{
    public static class StringExtensionMethods
    {
        public static string Quoted(this string text)
        {
            return string.Format("'{0}'", text);
        }

        internal static string And(this string text, string value)
        {
            if (string.IsNullOrEmpty(text))
                return value;

            if (string.IsNullOrEmpty(value))
                return text;

            return string.Join(" ", text, "And", value).Trim();
        }

        public static string Bracketed(this string text)
        {
            return string.Format("[{0}]", text);
        }

        public static string FromClause(this string[] collection, string joinOp = ",")
        {
            return string.Join(joinOp, collection.Select( item => item.Bracketed() ));

        }

    }
}
