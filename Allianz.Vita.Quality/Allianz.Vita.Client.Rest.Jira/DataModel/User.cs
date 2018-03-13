using Allianz.Vita.Client.Rest.Jira.Api2.Issue.Response;

namespace Allianz.Vita.Client.Rest.Jira.DataModel
{
    public class User : Field
    {
        public string Email { get; }
        public string DisplayName { get; }
        public bool Active { get; }

        public User(IssueResponse.JiraUser item) : base(item)
        {
           Email = item.emailAddress;
           DisplayName = item.displayname;
           Active = item.active;
            // assigne.avatarUrls;           
        }

        public override sealed string ToString()
        {
            return DisplayName ?? Name;
        }
        
    }
}
