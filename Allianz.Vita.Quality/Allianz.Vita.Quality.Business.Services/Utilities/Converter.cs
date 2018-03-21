using System;
using System.Collections.Generic;

namespace Allianz.Vita.Quality.Business.Services.Utilities
{
    static class Converter
    {
        public static DateTime? ToDateTimeNullable(this Dictionary<string, object> dict, string key)
        {
            DateTime? result = null;

            if (dict.ContainsKey(key))
            {
                DateTime.TryParse(dict[key].ToString(), out DateTime resultValue);
                result = resultValue;
            }

            return result;

        }

        public static bool? ToBooleanNullable(this Dictionary<string, object> dict, string key)
        {
            bool? result = null;

            if (dict.ContainsKey(key))
            {
                bool.TryParse(dict[key].ToString(), out bool resultValue);
                result = resultValue;
            }

            return result;
        }

        public static string ToValueString(this Dictionary<string, object> dict, string key)
        {
            return dict.ContainsKey(key) ? dict[key].ToString() : string.Empty;
        }

    }
}
