namespace Allianz.Vita.Client.Rest.Jira.Api2.Request
{
    /// <summary>
    /// Searches for issues using JQL. 
    /// 
    /// jql string
    /// 
    /// a JQL query string
    /// startAt int
    /// 
    /// the index of the first issue to return (0-based)
    /// maxResults int
    /// 
    /// the maximum number of issues to return (defaults to 50). The maximum allowable value is dictated by the JIRA property 'jira.search.views.default.max'. If you specify a value that is higher than this number, your search results will be truncated.
    /// validateQuery boolean
    /// 
    /// 
    /// Default: true
    /// 
    /// 
    /// 
    /// whether to validate the JQL query
    /// fields  string
    /// 
    /// the list of fields to return for each issue. By default, all navigable fields are returned.
    /// expand  string
    /// 
    /// A comma-separated list of the parameters to expand.
    /// </summary>
    public class SearchRequest : DataModel.Intefaces.RequestItem
    {
        public string[] fields;
        public int startAt;
        public int maxResults;
        public string jql;
        public bool validateQuery = true;

        public sealed override string ToString()
        {
            return string.Join(",", "fields=" + (fields == null ? Issue.Request.IssueRequest.Fields.All : string.Join(",", fields)),
                                    "startAt=" + startAt,
                                    "maxResults=" + maxResults,
                                    "jql=" + jql );
        }

        public static class Fields
        {
            public static string[] All = null;
        }

    }
    
}
