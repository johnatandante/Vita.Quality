using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allianz.Vita.Client.Rest.Jira.DataModel
{    
    public class Issue
    {
        public User Assignee { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ResolutionDate { get; set; }
        public Project Project { get; set; }
        public string Id { get; set; }
        public Priority Priority { get; set; }
        public string Summary { get; set; }
        public Status Status { get; set; }
        public IssueType IssueType { get; set; }
        public string Url { get; internal set; }
        public string Key { get; internal set; }
    }
}
