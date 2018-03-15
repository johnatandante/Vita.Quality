using Allianz.Vita.Client.Rest.Jira.Api2.Issue.Response;
using Allianz.Vita.Client.Rest.Jira.DataModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Allianz.Vita.Client.Rest.Jira.Utility
{
    public class Converter
    {

        public static Issue ToIssue(IssueResponse input)
        {
            return new Issue(input);
        }

        internal static T Unpack<T>(JObject item)
        {
            return JsonConvert.DeserializeObject<T>(item.ToString());
        }
        
    }
}
