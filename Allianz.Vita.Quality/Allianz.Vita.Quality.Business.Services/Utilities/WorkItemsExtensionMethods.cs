using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;

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

        public static bool Exist(this LinkCollection collection, int id )
        {
            bool result = false;
            foreach(RelatedLink item in collection)
            {
                if(item.RelatedWorkItemId == id)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public static IEnumerable<string> ToEnumerableStringValues(this NodeCollection nodeCollection, bool deepMode = true)
        {
            List<string> nodeList = new List<string>();
            foreach (Node node in nodeCollection)
            {
                nodeList.Add(node.Path);
                if (deepMode && node.HasChildNodes)
                    nodeList.AddRange(node.ChildNodes.ToEnumerableStringValues());
            }

            return nodeList;
        }

    }
}
