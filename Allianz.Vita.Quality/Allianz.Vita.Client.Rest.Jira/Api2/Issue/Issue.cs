using Allianz.Vita.Client.Rest.Jira.Api2.Issue.Request;
using System.Net.Http;
using System.Threading.Tasks;

namespace Allianz.Vita.Client.Rest.Jira.Api2.Issue
{
    class Issue : RestClient
    {

        static string SessionPath = "rest/api/2/issue/";
        
        public Issue(HttpClient httpClient = null) 
            : base(httpClient)
        {
            
        }

        internal async Task<DataModel.Issue> Get(string idOrKey)
        {

            DataModel.Issue issue = new DataModel.Issue();

            IssueRequest requestItem = new IssueRequest() { fields = IssueRequest.Fields.All, expand = bool.FalseString, properties = string.Empty };
            string path = Issue.SessionPath + idOrKey + "?" + requestItem.ToString();
            HttpResponseMessage response = await Client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                Response.IssueResponse resultItem = await response.Content.ReadAsAsync<Response.IssueResponse>();
                issue.Id = resultItem.id;
                issue.Url = resultItem.self;
                issue.Key = resultItem.key;
                issue.CreatedDate = resultItem.fields.created;
                issue.ResolutionDate = resultItem.fields.resolutiondate;
                issue.Summary = resultItem.fields.summary;

                issue.Assignee = new DataModel.User(resultItem.fields.assignee);
                issue.IssueType = new DataModel.IssueType(resultItem.fields.issuetype);
                issue.Project = new DataModel.Project(resultItem.fields.project);
                issue.Status = new DataModel.Status(resultItem.fields.status);
                issue.Priority = new DataModel.Priority(resultItem.fields.priority);
                
            }
            else
            {
                issue.Id = string.Empty;
            }

            return issue;
            
        }
    }
}
