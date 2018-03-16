using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allianz.Vita.Client.Rest.Jira.DataModel
{
    public class Configuration
    {
        public bool VotingEnabled { get; internal set; }
        public bool WatchingEnabled { get; internal set; }
        public bool UnassignedIssuesAllowed { get; internal set; }
        public bool SubTasksEnabled { get; internal set; }
        public bool IssueLinkingEnabled { get; internal set; }
        public bool TimeTrackingEnabled { get; internal set; }
        public bool AttachmentsEnabled { get; internal set; }
        public int WorkingHoursPerDay { get; internal set; }
        public int WorkingDaysPerWeek { get; internal set; }
        public string TimeTrackingTimeFormat { get; internal set; }
        public string TimeTrackingDefaultUnit { get; internal set; }
        
    }
}
