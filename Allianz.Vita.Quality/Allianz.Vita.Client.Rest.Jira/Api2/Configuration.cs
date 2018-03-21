using System.Net.Http;
using System.Threading.Tasks;

namespace Allianz.Vita.Client.Rest.Jira.Api2
{
    class Configuration : RestClient
    {

        static string Path = "rest/api/2/configuration";

        public Configuration(HttpClient client)
            : base(client)
        {

        }

        internal async Task<DataModel.Configuration> Get()
        {

            DataModel.Configuration config = new DataModel.Configuration();

            HttpResponseMessage response = await Client.GetAsync(Configuration.Path);            
            if (response.IsSuccessStatusCode)
            {
                Response.ConfigurationResponse resultItem = await response.Content.ReadAsAsync<Response.ConfigurationResponse>();

                config.VotingEnabled = resultItem.votingEnabled;
                config.WatchingEnabled = resultItem.watchingEnabled;
                config.UnassignedIssuesAllowed = resultItem.unassignedIssuesAllowed;
                config.SubTasksEnabled = resultItem.subTasksEnabled;
                config.IssueLinkingEnabled = resultItem.issueLinkingEnabled;
                config.TimeTrackingEnabled = resultItem.timeTrackingEnabled;
                config.AttachmentsEnabled = resultItem.attachmentsEnabled;

                config.WorkingHoursPerDay = resultItem.timeTrackingConfiguration.workingHoursPerDay;
                config.WorkingDaysPerWeek = resultItem.timeTrackingConfiguration.workingDaysPerWeek;
                config.TimeTrackingTimeFormat = resultItem.timeTrackingConfiguration.timeFormat;
                config.TimeTrackingDefaultUnit = resultItem.timeTrackingConfiguration.defaultUnit;

            }
            else if(response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
            {
                response.EnsureSuccessStatusCode();
            }

            return config;

        }
        
    }
}
