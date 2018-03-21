using Allianz.Vita.Client.Rest.Jira.Api2.Request;
using Allianz.Vita.Client.Rest.Jira.Utility;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Allianz.Vita.Client.Rest.Jira.Api2
{
    class Search : RestClient
    {
        static string Path = "rest/api/2/search";

        public Search(HttpClient httpClient = null) 
            : base(httpClient)
        {

        }

        internal async Task<DataModel.Search> Get(string jql, int startAt = 0, int maxResults = 0, string fields = "")
        {

            DataModel.Search item;
            
            SearchRequest requestItem = new SearchRequest() { jql = jql, fields = string.IsNullOrEmpty(fields) ? SearchRequest.Fields.All : fields.Split(','), startAt = startAt, maxResults = maxResults };
            string path = Search.Path.UrlGetCombine(requestItem);

            if (path.IsComplexQuery() || path.ExcedUrlLimit()) return await Post(jql, startAt, maxResults, fields);

            HttpResponseMessage response = await Client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                Response.SearchResponse resultItem = await response.Content.ReadAsAsync<Response.SearchResponse>();
                item = new DataModel.Search(resultItem);

            }
            else
            {
                item = new DataModel.Search() { Error = response.StatusCode.ToString() };
            }

            return item;

        }

        internal async Task<DataModel.Search> Post(string jql, int startAt = 0, int maxResults = 0, string fields = "")
        {

            DataModel.Search item;
            
            SearchRequest requestItem = new SearchRequest() { jql = jql, fields = string.IsNullOrEmpty(fields) ? SearchRequest.Fields.All : fields.Split(','), startAt = startAt, maxResults = maxResults };

            var json = JsonConvert.SerializeObject(requestItem);

            System.Console.WriteLine(json);

             HttpResponseMessage response = await Client.PostAsJsonAsync<SearchRequest>(Search.Path, requestItem);
            if (response.IsSuccessStatusCode)
            {
                Response.SearchResponse resultItem = await response.Content.ReadAsAsync<Response.SearchResponse>();
                item = new DataModel.Search(resultItem);
                
            }
            else
            {
                item = new DataModel.Search();
            }
            
            return item;

        }
    }
}
