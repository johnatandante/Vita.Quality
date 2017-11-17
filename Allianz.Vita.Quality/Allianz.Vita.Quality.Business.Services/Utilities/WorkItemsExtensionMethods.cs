using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;

namespace Allianz.Vita.Quality.Business.Services.Utilities
{
    static class WorkItemsExtensionMethods
    {
        public static string TryToGetField(this WorkItem workItem, string field)
        {
            object value;
            try
            {
                value = workItem.Fields[field].Value;
            }
            catch
            {
                value = string.Empty;
            }

            return value != null ? value.ToString() : string.Empty;

        }

        public static TEnum TryToGetEnumField<TEnum>(this WorkItem workItem, string field) 
            where TEnum : struct
        {

            if (!Enum.TryParse<TEnum>(workItem.TryToGetField(field), out TEnum result))
            {
                result = default(TEnum);
            }

            return result;
        }


    }
}
