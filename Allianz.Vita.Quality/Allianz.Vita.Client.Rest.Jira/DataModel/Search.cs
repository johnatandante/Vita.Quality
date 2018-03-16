using Allianz.Vita.Client.Rest.Jira.Api2.Response;
using Allianz.Vita.Client.Rest.Jira.Utility;
using System.Collections.Generic;
using System.Linq;

namespace Allianz.Vita.Client.Rest.Jira.DataModel
{
    public class Search
    {

        public Search(SearchResponse resultItem) : this(resultItem.total)
        {
            StartAt = resultItem.startAt;
            MaxResults = resultItem.maxResults;
            
            if (resultItem.issues != null)
            {
                Issues.AddRange( resultItem.issues.ToList().ConvertAll<Issue>(Converter.ToIssue));
                
            }

        }

        public Search(int total = 0)
        {
            Total = total;
            Issues = new List<Issue>();
        }

        public List<Issue> Issues { get; }
        public int StartAt { get; }
        public int MaxResults { get; }
        public int Total { get; }
        public string Error { get; set; }
    }
}
