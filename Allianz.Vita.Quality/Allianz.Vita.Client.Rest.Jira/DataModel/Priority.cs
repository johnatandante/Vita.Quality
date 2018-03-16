using Allianz.Vita.Client.Rest.Jira.Api2.Issue.Response;

namespace Allianz.Vita.Client.Rest.Jira.DataModel
{
    public class Priority : Field
    {

        public string Id { get; }
        
        public Priority(IssueResponse.Priority item) : base(item)
        {
            Id = item.id;
        }
        
    }
}
