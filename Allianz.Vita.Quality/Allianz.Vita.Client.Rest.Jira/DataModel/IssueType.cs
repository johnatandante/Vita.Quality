using Allianz.Vita.Client.Rest.Jira.Api2.Issue.Response;

namespace Allianz.Vita.Client.Rest.Jira.DataModel
{
    public class IssueType : Field
    {
        public IssueType(IssueResponse.Issuetype item ) : base(item)
        {
            Description = item.description;
            Id = item.id;
        }
        
        public string Description { get; }
        public string Id { get; }
    }
}
