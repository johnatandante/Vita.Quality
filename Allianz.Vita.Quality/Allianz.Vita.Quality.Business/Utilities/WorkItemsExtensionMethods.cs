using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allianz.Vita.Quality.Business.Utilities
{
    static class WorkItemsExtensionMethods
    {
        public static string TryToGetField(this WorkItem workItem, string field)
        {

            if (!workItem.Fields.TryGetValue(field, out object value))
                value = string.Empty;

            return value.ToString();

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
