using Allianz.Vita.Client.Rest.Jira.Api2.Issue.Response;

namespace Allianz.Vita.Client.Rest.Jira.DataModel
{
    public class User
    {
        public string Email { get; }
        public string DisplayName { get; }
        public bool Active { get; }
        public string Name { get; }
        public string Url { get; }

        public User(IssueResponse.JiraUser item)
        {
            Name = item.key;
            Url = item.self;
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
