using Allianz.Vita.Client.Rest.Jira.Api2.Issue.Response;
using Allianz.Vita.Client.Rest.Jira.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Allianz.Vita.Client.Rest.Jira.DataModel
{
    public class Issue
    {

        public Issue(string id = "")
        {
            Id = id;
            CustomFields = new Dictionary<string, object>();
        }

        public Issue(IssueResponse resultItem) : this(resultItem.id)
        {
            Url = resultItem.self;
            Key = resultItem.key;

            // main fields
            CreatedDate = resultItem.fields.created;
            ResolutionDate = resultItem.fields.resolutiondate;
            Summary = resultItem.fields.summary;

            if (resultItem.fields.assignee != null)
                Assignee = new User(Converter.Unpack<IssueResponse.JiraUser>(resultItem.fields.assignee));
            IssueType = new IssueType(Converter.Unpack<IssueResponse.Issuetype>(resultItem.fields.issuetype));
            Project = new Project(Converter.Unpack<IssueResponse.Project>(resultItem.fields.project));
            Status = new Status(Converter.Unpack<IssueResponse.Status>(resultItem.fields.status));
            if (resultItem.fields.priority != null)
                Priority = new Priority(Converter.Unpack<IssueResponse.Priority>(resultItem.fields.priority));

            // all the stuffs
            IDictionary<string, JToken> obj = (IDictionary<string, JToken>)resultItem.fields;
            CustomFields = obj.ToDictionary(pair => pair.Key, pair => pair.Value as object);

        }

        public User Assignee { get; }
        public DateTime? CreatedDate { get; }
        public DateTime? ResolutionDate { get; }
        public Project Project { get; }
        public string Id { get; }
        public Priority Priority { get; }
        public string Summary { get; }
        public Status Status { get; }
        public IssueType IssueType { get; }
        public string Url { get; }
        public string Key { get; }

        public Dictionary<string, object> CustomFields { get; }

    }
}
