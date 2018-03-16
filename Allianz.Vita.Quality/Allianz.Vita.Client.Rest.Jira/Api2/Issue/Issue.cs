using Allianz.Vita.Client.Rest.Jira.Api2.Issue.Request;
using Allianz.Vita.Client.Rest.Jira.Utility;
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

            DataModel.Issue issue;

            IssueRequest requestItem = new IssueRequest() { fields = IssueRequest.Fields.All, properties = string.Empty };
            string path = (Issue.SessionPath + idOrKey) .UrlGetCombine(requestItem);
            HttpResponseMessage response = await Client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                Response.IssueResponse resultItem = await response.Content.ReadAsAsync<Response.IssueResponse>();
                issue = new DataModel.Issue(resultItem);
            }
            else
            {
                issue = new DataModel.Issue();
            }

            return issue;
            
        }
    }
}
