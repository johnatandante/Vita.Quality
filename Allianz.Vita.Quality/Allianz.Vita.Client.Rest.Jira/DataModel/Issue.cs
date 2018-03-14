using System;

namespace Allianz.Vita.Client.Rest.Jira.DataModel
{
    public class Issue
    {
        public User Assignee { get; internal set; }
        public DateTime? CreatedDate { get; internal set; }
        public DateTime? ResolutionDate { get; internal set; }
        public Project Project { get; internal set; }
        public string Id { get; internal set; }
        public Priority Priority { get; internal set; }
        public string Summary { get; internal set; }
        public Status Status { get; internal set; }
        public IssueType IssueType { get; internal set; }
        public string Url { get; internal set; }
        public string Key { get; internal set; }
    }
}
