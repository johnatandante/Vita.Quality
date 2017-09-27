namespace System
{
    static class StringExtensionMethods
    {
        public static string Quoted(this string text)
        {
            return string.Format("'{0}'", text);
        }

    }
}
