using Allianz.Vita.Client.Rest.Jira.Api2.Issue.Response;

namespace Allianz.Vita.Client.Rest.Jira.DataModel
{
    public class Status : Field
    {

        public Status(IssueResponse.Status item) : base(item)
        {
            Id = item.id;   
        }

        public string Id { get; }

    }
}
