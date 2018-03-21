namespace Allianz.Vita.Client.Rest.Jira.Api2.Response
{
    /// <summary>
    /// Returns the information if the optional features in JIRA are enabled or disabled. If the time tracking is enabled, it also returns the detailed information about time tracking configuration.
    ///{
    ///    "votingEnabled": true,
    ///    "watchingEnabled": true,
    ///    "unassignedIssuesAllowed": false,
    ///    "subTasksEnabled": false,
    ///    "issueLinkingEnabled": true,
    ///    "timeTrackingEnabled": true,
    ///    "attachmentsEnabled": true,
    ///    "timeTrackingConfiguration": {
    ///        "workingHoursPerDay": 8,
    ///        "workingDaysPerWeek": 5,
    ///        "timeFormat": "pretty",
    ///        "defaultUnit": "day"
    ///    }
    ///}
    /// </summary>
    public class ConfigurationResponse
    {

        public bool votingEnabled;
        public bool watchingEnabled;
        public bool unassignedIssuesAllowed;
        public bool subTasksEnabled;
        public bool issueLinkingEnabled;
        public bool timeTrackingEnabled;
        public bool attachmentsEnabled;

        public TimeTrackingConfiguration timeTrackingConfiguration;
        
        public class TimeTrackingConfiguration
        {
            public int workingHoursPerDay;
            public int workingDaysPerWeek;
            public string timeFormat;
            public string defaultUnit;
        
        }

    }
}
