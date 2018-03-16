namespace Allianz.Vita.Client.Rest.Jira.Api2.Response
{
    public class SearchResponse
    {
        public string expand;
        public int startAt;
        public int maxResults;
        public int total;

        public Issue.Response.IssueResponse[] issues;

    }
}
