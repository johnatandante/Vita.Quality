using Allianz.Vita.Client.Rest.Jira.Api2.Issue.Response;

namespace Allianz.Vita.Client.Rest.Jira.DataModel
{
    public class Project : Field
    {

        public Project(IssueResponse.Project item) : base(item)
        {            
            Id = item.id;
            Key = item.key;
        }
        
        public string Id { get; }
        public string Key { get; }

    }
}
